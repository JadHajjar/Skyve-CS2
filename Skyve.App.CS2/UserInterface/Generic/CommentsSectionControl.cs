using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
public partial class CommentsSectionControl : SlickControl
{
	private IModCommentsInfo? modCommentsInfo;
	private IModChangelog[]? changelogs;
	private bool isLoading;
	private int page = 1;
	private bool noMorePages;

	public IPackageIdentity? Package { get; set; }

	public CommentsSectionControl()
	{
		InitializeComponent();

		C_UserIcon.User = ServiceCenter.Get<IUserService>().User;
		L_LoggedInAs.Text = Locale.LoggedInUser.Format(C_UserIcon.User.Name);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		Task.Run(() => LoadComments());
	}

	protected override void UIChanged()
	{
		B_Send.Font = UI.Font(9F);
		B_Send.Padding = UI.Scale(new Padding(6));
		TB_Message.Margin = L_LoggedInAs.Margin = C_UserIcon.Margin = B_Send.Margin = UI.Scale(new Padding(3));
		L_LoggedInAs.Font = UI.Font(7F, FontStyle.Bold);
		TB_Message.Height = UI.Scale(48) - L_LoggedInAs.Height - L_LoggedInAs.Margin.Horizontal;
		C_UserIcon.Size = UI.Scale(new Size(48, 48));
		PB_Loading.Size = UI.Scale(new Size(28, 28));
		PB_Loading.Location = new Point((Width - PB_Loading.Width) / 2, UI.Scale(175));
		spacer.Height = UI.Scale(1);
		spacer.Margin = UI.Scale(new Padding(5, 5, 5, 0));
		TLP_SendMessage.Padding = UI.Scale(new Padding(12, 5, 12, 0));
	}

	private void Scrollbar_Scroll(object sender, ScrollEventArgs e)
	{
		if (e.NewValue == 100 && !(Package is null || isLoading || noMorePages))
		{
			Task.Run(() => LoadComments(page + 1));
		}
	}

	private async void LoadComments(int page = 1)
	{
		if (Package is null || isLoading || noMorePages)
		{
			return;
		}

		try
		{
			App.Program.MainForm.CurrentPanel.StartLoader();
			isLoading = true;
			modCommentsInfo = await ServiceCenter.Get<IWorkshopService>().GetComments(Package, page);
			changelogs = (await ServiceCenter.Get<IWorkshopService>().GetInfoAsync(Package))?.Changelog.OrderBy(x => x.ReleasedDate).ToArray();
			noMorePages = !(modCommentsInfo?.HasMore ?? false);
			App.Program.MainForm.CurrentPanel.StopLoader();
		}
		catch { }

		this.TryInvoke(() => TLP_SendMessage.Visible = modCommentsInfo?.CanPost ?? false);

		if ((modCommentsInfo?.Posts) != null)
		{
			this.page = page;

			this.TryInvoke(() =>
			{
				PB_Loading.Visible = false;

				var currentHeight = P_Comments.Height;

				P_Comments.SuspendDrawing();
				using var img = new Bitmap(1, 1);
				using var g = Graphics.FromImage(img);
				g.SetUp();
				foreach (var item in modCommentsInfo.Posts)
				{
					var control = new CommentControl(item, Package, changelogs?.LastOrDefault(x => item.Created >= x.ReleasedDate)?.Version) { Dock = DockStyle.Top };
					control.Reply += OnReply;
					control.SetSize(g, Size);

					P_Comments.Controls.Add(control);
					P_Comments.Controls.SetChildIndex(control, 0);
				}

				P_Comments.ResumeDrawing();
				slickScroll1.SetPercentage(100D * currentHeight / (P_Comments.Height - P_Comments.Parent.Height), true);
				panel1.PerformLayout();
			});
		}

		isLoading = false;
	}

	private void OnReply(object sender, EventArgs e)
	{
		var quote = $"[Quote@{(sender as CommentControl)!.Comment.PostId}]\r\n\r";
		TB_Message.Text += TB_Message.Text.Length == 0 ? quote : $"\r\n{quote}";
		TB_Message.Focus();
		TB_Message_Enter(sender, e);

		this.TryBeginInvoke(() => TB_Message.Select(int.MaxValue, 1));
	}

	private async void B_Send_Click(object sender, EventArgs e)
	{
		if (Package is null || string.IsNullOrWhiteSpace(TB_Message.Text))
		{
			return;
		}

		B_Send.Loading = true;

		var post = await ServiceCenter.Get<IWorkshopService>().PostNewComment(Package, TB_Message.Text.RegexReplace(@"\[Quote@(\d+)\]", ReplaceQuotes));

		B_Send.Loading = false;
		if (post == null)
		{
			//
			return;
		}

		TB_Message.Text = string.Empty;
		TB_Message_Enter(sender, e);

		var control = new CommentControl(post, Package, Package.GetWorkshopInfo()?.Version) { Dock = DockStyle.Top };

		P_Comments.Controls.Add(control);

		SlickScroll.GlobalScrollTo(control);
	}

	private string ReplaceQuotes(Match match)
	{
		var id = match.Groups[1].Value.SmartParse();
		var comment = P_Comments.Controls.OfType<CommentControl>().FirstOrDefault(x => x.Comment.PostId == id);

		if (comment != null)
		{
			return $"[QUOTE='{comment.Comment.Username}, post: {comment.Comment.PostId}, member: {comment.Comment.UserId}']{comment.Comment.Message}[/QUOTE]\r\n";
		}

		return string.Empty;
	}

	private void TB_Message_Enter(object sender, EventArgs e)
	{
		this.TryBeginInvoke(() =>
		{
			var isOpen = TB_Message.Focused || !string.IsNullOrWhiteSpace(TB_Message.Text) || FLP_FormatButtons.Controls.Any(x => x.Focused);

			FLP_FormatButtons.Visible = isOpen;

			AnimationHandler.Animate(TB_Message
				, new Size(0, isOpen ? UI.Scale(150) : UI.Scale(48) - L_LoggedInAs.Height - L_LoggedInAs.Margin.Horizontal)
				, 2.5
				, AnimationOption.IgnoreWidth);
		});
	}

	private void TB_Message_TextChanged(object sender, EventArgs e)
	{
		B_Send.ButtonType = !string.IsNullOrWhiteSpace(TB_Message.Text) ? ButtonType.Active : ButtonType.Normal;
		B_Send.Enabled = !string.IsNullOrWhiteSpace(TB_Message.Text);
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.Escape && (!string.IsNullOrWhiteSpace(TB_Message.Text) || TB_Message.Focused))
		{
			TB_Message.Text = string.Empty;
			P_Comments.Focus();
			TB_Message_Enter(this, EventArgs.Empty);

			return true;
		}

		if (keyData == Keys.Enter && TB_Message.Focused)
		{
			SendKeys.Send("+{ENTER}");

			return true;
		}

		switch (keyData)
		{
			case Keys.Control | Keys.B:
				B_Bold_Click(this, EventArgs.Empty);
				return true;
			case Keys.Control | Keys.I:
				B_Italic_Click(this, EventArgs.Empty);
				return true;
			case Keys.Control | Keys.U:
				B_Underline_Click(this, EventArgs.Empty);
				return true;
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void B_Bold_Click(object sender, EventArgs e)
	{
		WrapSelection("B");
	}

	private void B_Italic_Click(object sender, EventArgs e)
	{
		WrapSelection("I");
	}

	private void B_Underline_Click(object sender, EventArgs e)
	{
		WrapSelection("U");
	}

	private void B_Link_Click(object sender, EventArgs e)
	{
		var prompt = MessagePrompt.ShowInput(string.Empty, "Enter URL");
		if (prompt.DialogResult == DialogResult.OK)
		{
			var text = $"[URL=\"{prompt.Input}\"]{TB_Message.SelectedText.Trim()}[/URL]";

			Append(text, text.Length - (string.IsNullOrWhiteSpace(TB_Message.SelectedText) ? 6 : 0));
		}
	}

	private void B_List_Click(object sender, EventArgs e)
	{
		Append("\r\n[LIST]\r\n[*] \r\n[*] \r\n[*] \r\n[/LIST]", 14);
	}

	private void B_NumberedList_Click(object sender, EventArgs e)
	{
		Append("\r\n[LIST=1]\r\n[*] \n[*] \r\n[*] \r\n[/LIST]", 15);
	}

	private void WrapSelection(string text)
	{
		var selectedText = TB_Message.SelectedText;
		var firstPart = TB_Message.Text.Substring(0, TB_Message.SelectionStart);
		var secondPart = TB_Message.Text.Substring(TB_Message.SelectionStart + TB_Message.SelectionLength);

		if (selectedText.LastOrDefault() is ' ')
		{
			selectedText = selectedText.Substring(0, selectedText.Length - 1);
			secondPart = ' ' + secondPart;
		}

		var addedText = $"[{text}]{selectedText}[/{text}]";

		TB_Message.Text = firstPart + addedText + secondPart;
		TB_Message.Focus();

		this.TryBeginInvoke(() =>
		{
			TB_Message.Select(firstPart.Length + $"[{text}]".Length, addedText.Length - ($"[{text}]".Length * 2) - 1);
		});
	}

	private void Append(string text, int focus)
	{
		var selectedText = TB_Message.SelectedText;
		var firstPart = TB_Message.Text.Substring(0, TB_Message.SelectionStart);
		var secondPart = TB_Message.Text.Substring(TB_Message.SelectionStart + TB_Message.SelectionLength);

		if (selectedText.LastOrDefault() is ' ')
		{
			secondPart = ' ' + secondPart;
		}

		TB_Message.Text = firstPart + text + secondPart;
		TB_Message.Focus();

		this.TryBeginInvoke(() =>
		{
			TB_Message.Select(firstPart.Length + focus, 0);
		});
	}
}
