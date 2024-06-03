using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
public partial class CommentsSectionControl : UserControl
{
	private IModCommentsInfo? modCommentsInfo;
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

		TB_Message.Margin = L_LoggedInAs.Margin = C_UserIcon.Margin = B_Send.Margin = UI.Scale(new Padding(3));
		L_LoggedInAs.Font = UI.Font(7F, FontStyle.Bold);
		TB_Message.Height = UI.Scale(48) - L_LoggedInAs.Height - L_LoggedInAs.Margin.Horizontal;
		C_UserIcon.Size = UI.Scale(new Size(48, 48));
		PB_Loading.Size = UI.Scale(new Size(28, 28));
		PB_Loading.Location = new Point((Width - PB_Loading.Width) / 2, UI.Scale(175));
		spacer.Height = UI.Scale(1);
		spacer.Margin = TLP_SendMessage.Padding = UI.Scale(new Padding(5, 5, 5, 0));

		Task.Run(() => LoadComments());
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

		PanelContent.GetParentPanel(this).StartLoader();
		isLoading = true;
		modCommentsInfo = await ServiceCenter.Get<IWorkshopService>().GetComments(Package, page);
		noMorePages = !(modCommentsInfo?.HasMore ?? false);

		PanelContent.GetParentPanel(this).StopLoader();
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
					var control = new CommentControl(item, Package) { Dock = DockStyle.Top };
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
		var control = new CommentControl(post, Package) { Dock = DockStyle.Top };

		P_Comments.Controls.Add(control);

		SlickScroll.GlobalScrollTo(control);
	}

	private string ReplaceQuotes(Match match)
	{
		var id = match.Groups[1].Value.SmartParse();
		var comment = P_Comments.Controls.OfType<CommentControl>().FirstOrDefault(x => x.Comment.PostId == id);

		if (comment != null)
		{
			return $"[QUOTE='{comment.Comment.Username}, post: {comment.Comment.PostId}, member: {comment.Comment.UserId}']{comment.Comment.Message}[/QUOTE]";
		}

		return string.Empty;
	}

	private void TB_Message_Enter(object sender, EventArgs e)
	{
		this.TryBeginInvoke(() => AnimationHandler.Animate(TB_Message, new Size(0,
			TB_Message.Focused || !string.IsNullOrWhiteSpace(TB_Message.Text)
			? UI.Scale(150)
			: UI.Scale(48) - L_LoggedInAs.Height - L_LoggedInAs.Margin.Horizontal), 2.5, AnimationOption.IgnoreWidth));
	}

	private void TB_Message_TextChanged(object sender, EventArgs e)
	{
		B_Send.Enabled = !string.IsNullOrWhiteSpace(TB_Message.Text);
	}
}
