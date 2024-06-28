using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Generic;
using Skyve.App.UserInterface.Panels;
using Skyve.App.Utilities;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
public partial class CommentControl : SlickControl
{
	private readonly IModComment _comment;
	private Dictionary<Rectangle, Action> actionRects = [];
	private StringBuilder commentText = new();

	public event EventHandler? Reply;

	public IModComment Comment => _comment;

	public CommentControl(IModComment comment, IPackageIdentity packageIdentity, string? versionNumber)
	{
		InitializeComponent();
		AutoSize = true;
		AutoInvalidate = false;

		_comment = comment;

		C_UserImage.User = ServiceCenter.Get<IUserService>().TryGetUser(_comment.Username);
		L_Author.Text = _comment.Username;
		L_Time.Text = _comment.Created.ToLocalTime().ToRelatedString();
		L_AuthorLabel.Visible = _comment.Username == packageIdentity.GetWorkshopInfo()?.Author?.Id?.ToString();
		L_Version.Text = versionNumber is null ? LocaleCS2.InitialRelease : $"v{versionNumber}";
		L_Version.CustomBackColor = versionNumber == packageIdentity.GetWorkshopInfo()?.Version ? FormDesign.Design.GreenColor : FormDesign.Design.InfoColor.MergeColor(FormDesign.Design.BackColor);

		var matches = Regex.Matches(_comment.Message, @"\[ATTACH.+?\](\d+)\[/ATTACH\]");

		foreach (Match match in matches)
		{
			FLP_Thumbnails.Controls.Add(new MiniThumbControl(new AttachmentThumbnail(match.Groups[1].Value))
			{
				Label = $"Attachment #{match.Groups[1].Value}",
				Size = UI.Scale(new Size(150, 100))
			});
		}
	}

	protected override void UIChanged()
	{
		TLP_Back.Padding = Padding = UI.Scale(new Padding(6));
		C_UserImage.Size = UI.Scale(new Size(48, 48));
		L_Version.Padding = L_Time.Padding = L_AuthorLabel.Padding = UI.Scale(new Padding(4, 2, 2, 2));
		L_Author.Font = UI.Font(9.75F, FontStyle.Bold);
		L_Version.Font = L_Time.Font = UI.Font(7F);
		L_Version.Margin = L_Time.Margin = UI.Scale(new Padding(3, 4, 0, 5));
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP_Back.BackColor = design.AccentBackColor;
		L_Time.CustomBackColor = design.InfoColor.MergeColor(design.BackColor, 25);
	}

	private void C_Message_MouseMove(object sender, MouseEventArgs e)
	{
		var hovered = actionRects.Any(x => x.Key.Contains(e.Location));

		C_Message.Cursor = hovered ? Cursors.Hand : Cursors.Default;
	}

	private void C_Message_MouseClick(object sender, MouseEventArgs e)
	{
		foreach (var item in actionRects)
		{
			if (item.Key.Contains(e.Location))
			{
				item.Value();
			}
		}
	}

	private void B_Copy_Click(object sender, EventArgs e)
	{
		Clipboard.SetText(commentText.ToString());
	}

	private void B_Reply_Click(object sender, EventArgs e)
	{
		Reply?.Invoke(this, e);
	}

	private void C_Message_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.SetUp(C_Message.BackColor);

		using var painter = new ForumPainter(e.Graphics, _comment, C_Message.Size);

		painter.Paint(out var height);

		actionRects = painter.Actions;
		commentText = painter.Text;

		C_Message.Height = height;
	}

	private void C_UserImage_Click(object sender, EventArgs e)
	{
		App.Program.MainForm.PushPanel(new PC_UserPage(C_UserImage.User!));
	}

	internal void SetSize(Graphics g, Size size)
	{
		using var painter = new ForumPainter(g, _comment, size + C_Message.Size - Size);

		painter.Paint(out var height);

		C_Message.Height = height;
	}

	private class ForumPainter(Graphics g, IModComment _comment, Size Size) : IDisposable
	{
		private readonly List<(ForumFormat Format, Dictionary<string, object> Options)> activeFormats = [];
		private readonly List<ForumFormat> validFormats = Enum.GetValues(typeof(ForumFormat)).Cast<ForumFormat>().ToList();
		private readonly SolidBrush defaultBrush = new(FormDesign.Design.ForeColor);
		private readonly Font Font = UI.Font(8.25F);
		private PointF location;
		private int activeNesting;
		private int lineHeight;
		private int workingIndex;

		public Dictionary<Rectangle, Action> Actions { get; } = [];
		public StringBuilder Text { get; } = new();

		public void Dispose()
		{
			defaultBrush.Dispose();
			Font.Dispose();
		}

		public void Paint(out int height)
		{
			validFormats.Remove(ForumFormat.None);

			while (workingIndex < _comment.Message.Length)
			{
				var c = _comment.Message[workingIndex];

				if (c is '[')
				{
					var isCloser = workingIndex + 1 < _comment.Message.Length && _comment.Message[workingIndex + 1] is '/';
					var formatText = GetFormat(workingIndex + (isCloser ? 2 : 1));
					var text = _comment.Message.Substring(workingIndex + (isCloser ? 2 : 1));
					var format = validFormats.FirstOrDefault(x => formatText == x.ToString());

					if (format is not ForumFormat.None)
					{
						var endIndex = _comment.Message.Substring(workingIndex + (isCloser ? 2 : 1)).IndexOf(']');

						if (endIndex > 0)
						{
							endIndex += workingIndex + (isCloser ? 2 : 1);

							if (isCloser)
							{
								RemoveFormat(format);
							}
							else
							{
								var formatLength = format.ToString().Length + 2;

								if (AddFormat(format, endIndex - formatLength - workingIndex > 0 ? _comment.Message.Substring(workingIndex + formatLength, endIndex - formatLength - workingIndex) : string.Empty))
								{
									DrawFormat(activeFormats.Last());
								}

								if (format is ForumFormat.QUOTE && _comment.Message[endIndex + 1] is '\n')
								{
									endIndex++;
								}
							}

							if (format is ForumFormat.QUOTE or ForumFormat.ICODE)
							{
								location.X = activeNesting = GetActiveNesting();
							}

							workingIndex = endIndex + 1;
							continue;
						}
					}

					if (workingIndex + 2 < _comment.Message.Length && _comment.Message[workingIndex + 1] is '*' && _comment.Message[workingIndex + 2] is ']' && activeFormats.Any(x => x.Format is ForumFormat.LIST))
					{
						var dic = activeFormats.Last(x => x.Format is ForumFormat.LIST).Options;

						location.X = 10 * (float)UI.FontScale;
						if ((bool)dic["Numbered"])
						{
							var count = 1 + (int)dic["Count"];
							DrawCharacter($"{count}.");
							dic["Count"] = count;
						}
						else
						{
							DrawCharacter("•");
						}

						location.X = (25 * (float)UI.FontScale) + activeNesting;
						workingIndex += 3;
						continue;
					}
				}

				var i = workingIndex;
				for (; workingIndex < _comment.Message.Length; workingIndex++)
				{
					if (activeFormats.Any(x => x.Format is ForumFormat.URL))
					{
						if (_comment.Message[workingIndex] is '[' or '\n')
						{
							if (i == workingIndex)
							{
								workingIndex++;
							}

							break;
						}
					}
					else if (_comment.Message[workingIndex] is ' ' or '\t' or '\r' or '[' or '\n')
					{
						if (i == workingIndex)
						{
							workingIndex++;
						}

						break;
					}
				}

				DrawCharacter(_comment.Message.Substring(i, workingIndex - i));
			}

			height = (int)(location.Y + Font.Height + (3 * UI.FontScale));
		}

		private int GetActiveNesting()
		{
			var icode = activeFormats.Any(x => x.Format is ForumFormat.ICODE) ? UI.Scale(10) : 0;

			for (var i = activeFormats.Count - 1; i >= 0; i--)
			{
				if (activeFormats[i].Format is ForumFormat.QUOTE)
				{
					return icode + (int)((12 * UI.FontScale) + (10 * UI.FontScale * (int)activeFormats[i].Options["Nesting"]));
				}
			}

			return icode;
		}

		private string GetFormat(int index)
		{
			var text = string.Empty;

			while (index < _comment.Message.Length && char.IsLetter(_comment.Message[index]))
			{
				text += _comment.Message[index++];
			}

			return text;
		}

		private bool AddFormat(ForumFormat format, string param)
		{
			switch (format)
			{
				case ForumFormat.SIZE:
					activeFormats.Add((format, new() { ["Size"] = param.SmartParse() }));
					return true;
				case ForumFormat.COLOR:
					var split = param.Split(',');
					activeFormats.Add((format, new() { ["Color"] = Color.FromArgb(split[0].SmartParse(), split[1].SmartParse(), split[2].SmartParse()) }));
					return true;
				case ForumFormat.LIST:
					activeFormats.Add((format, new() { ["Numbered"] = !string.IsNullOrEmpty(param), ["Count"] = 0 }));
					return true;
				case ForumFormat.URL:
					activeFormats.Add((format, new() { ["URL"] = param.Trim('\'') }));
					return true;
				case ForumFormat.ICODE:
					NewLine();
					NewLine();
					activeFormats.Add((format, new() { ["Start"] = location }));
					return true;
				case ForumFormat.QUOTE:
					var quoteSplit = param.Split(',');
					if (quoteSplit.Length < 2)
					{
						return false;
					}

					activeFormats.Add((format, new()
					{
						["Sender"] = quoteSplit[0].Substring(1),
						["Nesting"] = activeFormats.Count(x => x.Format is ForumFormat.QUOTE),
						["PostId"] = quoteSplit.FirstOrDefault(x => x.Trim().StartsWith("post:"))?.SmartParse() ?? 0
					}));
					return true;
			}

			activeFormats.Add((format, []));
			return true;
		}

		private void RemoveFormat(ForumFormat format)
		{
			var index = activeFormats.FindLastIndex(x => x.Format == format);

			if (index >= 0)
			{
				if (format is ForumFormat.ICODE)
				{
					NewLine();
					NewLine();

					using var pen = new Pen(FormDesign.Design.AccentColor, UI.Scale(1.5f));

					var start = (PointF)activeFormats[index].Options["Start"];
					g.DrawRoundedRectangle(pen, Rectangle.Round(RectangleF.FromLTRB(start.X + UI.Scale(5f), start.Y - (Font.Height / 2), Size.Width - UI.Scale(3f), location.Y - (Font.Height / 2))), UI.Scale(3));
				}
				else if (format is ForumFormat.QUOTE)
				{
					var nesting = (int)activeFormats[index].Options["Nesting"];

					if (nesting == 0)
					{
						g.FillRectangle(new SolidBrush(FormDesign.Design.AccentBackColor), new Rectangle(0, (int)(location.Y + lineHeight + (8 * UI.FontScale)), Size.Width, Size.Height));

						location.Y += lineHeight + UI.Scale(5f);
					}
					else
					{
						var diff = UI.Scale(5);

						location.Y += diff;

						DrawQuote((diff * 2) + UI.Scale(2), nesting - 1);
					}
				}

				activeFormats.RemoveAt(index);
			}
		}

		private void DrawFormat((ForumFormat Format, Dictionary<string, object> Options) value)
		{
			if (value.Format is ForumFormat.ATTACH)
			{
				DrawCharacter("ATTACHEMENT #");
				return;
			}

			if (value.Format is not ForumFormat.QUOTE)
			{
				return;
			}

			if (location.Y > 0)
			{
				location.Y += UI.Scale(10);
			}

			location.Y -= 6 * (float)UI.FontScale;
			DrawQuote(UI.Scale(48));
			location.Y += 6 * (float)UI.FontScale;

			var userImage = ServiceCenter.Get<IWorkshopService>().GetUser(ServiceCenter.Get<IUserService>().TryGetUser(value.Options["Sender"].ToString()))?.GetThumbnail();
			var avatarRect = new Rectangle(new((int)((7 + (15 * (int)value.Options["Nesting"])) * UI.FontScale), (int)(location.Y + (4 * UI.FontScale))), UI.Scale(new Size(18, 18)));

			if (userImage != null)
			{
				g.DrawRoundImage(userImage, avatarRect);
			}
			else
			{
				using var brush = new SolidBrush(UserIcon.GetUserColor(value.Options["Sender"].ToString()));
				using var icon = IconManager.GetIcon("User", avatarRect.Height).Color(brush.Color.GetTextColor());

				g.FillEllipse(brush, avatarRect);
				g.DrawImage(icon, avatarRect.CenterR(icon.Size));
			}

			avatarRect.X += avatarRect.Width + UI.Scale(5);
			avatarRect.Width = Size.Width - avatarRect.X;

			using var font = UI.Font(9F, FontStyle.Bold);
			using var format = new StringFormat { LineAlignment = StringAlignment.Center };
			g.DrawString(value.Options["Sender"].ToString(), font, defaultBrush, avatarRect, format);

			using var icon2 = IconManager.GetIcon("Reply", avatarRect.Height).Color(FormDesign.Design.IconColor);
			g.DrawImage(icon2, avatarRect.Pad(0, 0, UI.Scale(4), 0).Align(icon2.Size, ContentAlignment.TopRight));

			location = new PointF((int)(10 * (int)value.Options["Nesting"] * UI.FontScale), location.Y + UI.Scale(28));
		}

		private void DrawCharacter(string text)
		{
			Text.Append(text);

			if (text == " ")
			{
				return;
			}

			if (text == "\n")
			{
				NewLine();
				return;
			}

			var fontStyle = FontStyle.Regular;
			var fontSize = 8.25F;
			var fontFamily = UI.FontFamily;
			SolidBrush? solidBrush = null;

			if (activeFormats.Any(x => x.Format is ForumFormat.B or ForumFormat.USER))
			{
				fontStyle |= FontStyle.Bold;
			}

			if (activeFormats.Any(x => x.Format is ForumFormat.I))
			{
				fontStyle |= FontStyle.Italic;
			}

			if (activeFormats.Any(x => x.Format is ForumFormat.U or ForumFormat.URL))
			{
				fontStyle |= FontStyle.Underline;
			}

			if (activeFormats.Any(x => x.Format is ForumFormat.ICODE))
			{
				fontFamily = "Consolas";
			}

			if (activeFormats.Any(x => x.Format is ForumFormat.COLOR))
			{
				solidBrush = new SolidBrush((Color)activeFormats.Last(x => x.Format is ForumFormat.COLOR).Options["Color"]);
			}
			else if (activeFormats.Any(x => x.Format is ForumFormat.USER or ForumFormat.URL))
			{
				solidBrush = new SolidBrush(FormDesign.Design.ActiveColor);
			}

			if (activeFormats.Any(x => x.Format is ForumFormat.SIZE))
			{
				fontSize = (int)activeFormats.Last(x => x.Format is ForumFormat.SIZE).Options["Size"];
			}

			using var font = UI.Font(fontFamily, fontSize, fontStyle);
			var size = g.Measure(text.ToString(), font);

			lineHeight = Math.Max(lineHeight, font.Height);

			if (location.X + size.Width > Size.Width)
			{
				location = new PointF(0, location.Y + lineHeight + UI.Scale(1.5f));
				lineHeight = font.Height;

				if (activeFormats.Any(x => x.Format is ForumFormat.QUOTE))
				{
					DrawQuote(lineHeight);

					location.X = activeNesting;
				}
			}

			if (activeFormats.Any(x => x.Format is ForumFormat.URL))
			{
				var url = (string)activeFormats.Last(x => x.Format is ForumFormat.URL).Options["URL"];
				Actions[new Rectangle(Point.Round(location), Size.Round(size))] = () => PlatformUtil.OpenUrl(url);
			}

			g.DrawString(text, font, solidBrush ?? defaultBrush, location);

			location.X += size.Width;

			solidBrush?.Dispose();
		}

		private void NewLine()
		{
			location = new PointF(activeNesting, location.Y + lineHeight.If(0, Font.Height) + UI.Scale(1.5f));

			if (activeFormats.Any(x => x.Format is ForumFormat.QUOTE))
			{
				DrawQuote(lineHeight.If(0, Font.Height) + UI.Scale(2));
			}

			lineHeight = 0;
		}

		private void DrawQuote(int height, int? forcedNesting = null)
		{
			var nesting =  (int)(UI.FontScale * 15 * (forcedNesting ?? (int)activeFormats.Last(x => x.Format is ForumFormat.QUOTE).Options["Nesting"]));
			var diff = UI.Scale(5);
			using var brush1 = new SolidBrush(FormDesign.Design.ActiveColor);
			using var brush2 = new LinearGradientBrush(new Rectangle(1 + nesting, (int)location.Y + diff, Size.Width - nesting, height + (diff / 2) + 2), FormDesign.Design.ActiveColor.MergeColor(FormDesign.Design.AccentBackColor, 35 - (nesting / 2)), FormDesign.Design.ActiveColor.MergeColor(FormDesign.Design.AccentBackColor, 5), 0f);

			if (nesting > 0)
			{
				DrawQuote(height, (forcedNesting ?? (int)activeFormats.Last(x => x.Format is ForumFormat.QUOTE).Options["Nesting"]) - 1);
			}

			g.FillRectangle(brush2, new Rectangle(1 + nesting, (int)location.Y + diff, Size.Width - nesting, height + (diff / 2) + 2));
			g.FillRectangle(brush1, new Rectangle(0 + nesting, (int)location.Y + diff, UI.Scale(3), height + (diff / 2) + 2));
		}
	}

	private enum ForumFormat
	{
		None,
		B,
		I,
		U,
		SIZE,
		COLOR,
		ICODE,
		ISPOILER,
		LIST,
		URL,
		ATTACH,
		USER,
		QUOTE
	}

	private class AttachmentThumbnail(string id) : IThumbnailObject
	{
		public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
		{
			thumbnailUrl = $"https://forum.paradoxplaza.com/forum/attachments/{id}";
			thumbnail = imageService.GetImage(thumbnailUrl, true, $"Attachment_{id}.jpeg").Result;

			return true;
		}
	}

	private void B_Reply_SizeChanged(object sender, EventArgs e)
	{
		B_Copy.MinimumSize = new Size(B_Reply.Height, B_Reply.Height);
	}
}
