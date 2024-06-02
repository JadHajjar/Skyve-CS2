using Skyve.App.UserInterface.Content;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
public partial class CommentControl : SlickControl
{
	private readonly IModComment _comment;
	private readonly IPackageIdentity _packageIdentity;

	public CommentControl(IModComment comment, IPackageIdentity packageIdentity)
	{
		InitializeComponent();
		Dock = DockStyle.Top;
		AutoSize = true;

		_comment = comment;
		_packageIdentity = packageIdentity;

		C_UserImage.User = ServiceCenter.Get<IUserService>().TryGetUser(_comment.Username);
		L_Author.Text = _comment.Username;
		L_Time.Text = _comment.Created.ToRelatedString();
		L_AuthorLabel.Visible = _comment.Username == packageIdentity.GetWorkshopInfo()?.Author?.Id?.ToString();
	}

	protected override void UIChanged()
	{
		TLP_Back.Padding = Padding = UI.Scale(new Padding(6), UI.FontScale);
		C_UserImage.Size = UI.Scale(new Size(48, 48), UI.FontScale);
		L_AuthorLabel.Padding = UI.Scale(new Padding(2), UI.FontScale);
		L_Author.Font = UI.Font(9.75F, FontStyle.Bold);
		L_Time.Font = UI.Font(7.5F);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP_Back.BackColor = design.AccentBackColor;
		L_Time.ForeColor = design.LabelColor;
	}

	private void C_Message_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.SetUp(C_Message.BackColor);

		using var painter = new ForumPainter(e, _comment, C_Message.Size);

		painter.Paint(out var height);

		C_Message.Height = height;
	}

	private class ForumPainter(PaintEventArgs e, IModComment _comment, Size Size) : IDisposable
	{
		private readonly List<(ForumFormat Format, Dictionary<string, object> Options)> activeFormats = [];
		private readonly List<ForumFormat> validFormats = Enum.GetValues(typeof(ForumFormat)).Cast<ForumFormat>().ToList();
		private PointF location;
		private readonly SolidBrush defaultBrush = new(FormDesign.Design.ForeColor);
		private readonly Font Font = UI.Font(8.25F);
		private int activeNesting;
		private int lineHeight;
		private int workingIndex;

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
							}

							activeNesting = GetActiveNesting();
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

						location.X = 25 * (float)UI.FontScale;
						workingIndex += 3;
						continue;
					}
				}

				var i = workingIndex;
				for (; workingIndex < _comment.Message.Length; workingIndex++)
				{
					if (_comment.Message[workingIndex] is ' ' or '\t' or '\r' or '[' or '\n')
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
			for (var i =activeFormats.Count - 1; i >0; i--)
			{
				if (activeFormats[i].Format is ForumFormat.QUOTE)
					return (int)(5 * UI.FontScale + 10 * UI.FontScale * (int)activeFormats[i].Options["Nesting"]);
			}

			return 0;
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
				if (format is ForumFormat.QUOTE)
				{
					var nesting = (int)activeFormats[index].Options["Nesting"];

					if (nesting == 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.AccentBackColor), new Rectangle(0, (int)(location.Y +lineHeight+ (8 * UI.FontScale)), Size.Width, Size.Height));

						location.Y += lineHeight+ 5 * (float)UI.FontScale;
					}
					else
					{
						var diff = (int)(5 * UI.FontScale);

						location.Y += diff;
						
						DrawQuote(diff*2+1, nesting - 1);
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

			if ((int)value.Options["Nesting"] != 0)
			{
				location.Y += (int)(10 * UI.FontScale);
			}

			location.Y -= 6 * (float)UI.FontScale;
			DrawQuote((int)(40 * UI.FontScale));
			location.Y += 6 * (float)UI.FontScale;

			var userImage = ServiceCenter.Get<IWorkshopService>().GetUser(ServiceCenter.Get<IUserService>().TryGetUser(value.Options["Sender"].ToString()))?.GetThumbnail();
			var avatarRect = new Rectangle(new((int)((7 + 15 * (int)value.Options["Nesting"]) * UI.FontScale), (int)(location.Y + (4 * UI.FontScale))), UI.Scale(new Size(18, 18), UI.FontScale));

			if (userImage != null)
			{
				e.Graphics.DrawRoundImage(userImage, avatarRect);
			}
			else
			{
				using var brush = new SolidBrush(UserIcon.GetUserColor(value.Options["Sender"].ToString()));
				using var icon = IconManager.GetIcon("User", avatarRect.Height).Color(brush.Color.GetTextColor());

				e.Graphics.FillEllipse(brush, avatarRect);
				e.Graphics.DrawImage(icon, avatarRect.CenterR(icon.Size));
			}

			avatarRect.X += avatarRect.Width + (avatarRect.X / 2);
			avatarRect.Width = Size.Width - avatarRect.X;

			using var font = UI.Font(9F, FontStyle.Bold);
			using var format = new StringFormat { LineAlignment = StringAlignment.Center };
			e.Graphics.DrawString(value.Options["Sender"].ToString(), font, defaultBrush, avatarRect, format);

			using var icon2 = IconManager.GetIcon("Reply", avatarRect.Height).Color(FormDesign.Design.IconColor);
			e.Graphics.DrawImage(icon2, avatarRect.Pad(0, 0, (int)(4 * UI.FontScale), 0).Align(icon2.Size, ContentAlignment.TopRight));

			location = new PointF((int)(10 * (int)value.Options["Nesting"] * UI.FontScale), location.Y + (int)(22 * UI.FontScale));
		}

		private void DrawCharacter(string text)
		{
			if (text == " ")
			{
				return;
			}

			if (text == "\n")
			{
				location = new PointF(activeNesting, location.Y + lineHeight.If(0, Font.Height));

				if (activeFormats.Any(x => x.Format is ForumFormat.QUOTE))
				{
					DrawQuote(lineHeight.If(0, Font.Height));

					location.X += (int)(8 * UI.FontScale);
				}

				lineHeight = 0;
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

			if (activeFormats.Any(x => x.Format is ForumFormat.U))
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
			else if (activeFormats.Any(x => x.Format is ForumFormat.USER))
			{
				solidBrush = new SolidBrush(FormDesign.Design.ActiveColor);
			}

			if (activeFormats.Any(x => x.Format is ForumFormat.SIZE))
			{
				fontSize = (int)activeFormats.Last(x => x.Format is ForumFormat.SIZE).Options["Size"];
			}

			using var font = UI.Font(fontFamily, fontSize, fontStyle);
			var size = e.Graphics.Measure(text.ToString(), font);

			lineHeight = Math.Max(lineHeight, font.Height);

			if (location.X + size.Width > Size.Width)
			{
				location = new PointF(0, location.Y + lineHeight + (float)(2.5 * UI.FontScale));
				lineHeight = font.Height;

				if (activeFormats.Any(x => x.Format is ForumFormat.QUOTE))
				{
					DrawQuote(lineHeight);

					location.X = (int)(8 * UI.FontScale) + activeNesting;
				}
			}

			e.Graphics.DrawString(text, font, solidBrush ?? defaultBrush, location);

			location.X += size.Width;

			solidBrush?.Dispose();
		}

		private void DrawQuote(int height, int? forcedNesting = null)
		{
			var nesting = forcedNesting ?? (int)(UI.FontScale * 15 * (int)activeFormats.Last(x => x.Format is ForumFormat.QUOTE).Options["Nesting"]);
			var diff = (int)(5 * UI.FontScale);
			using var brush1 = new SolidBrush(FormDesign.Design.ActiveColor);
			using var brush2 = new LinearGradientBrush(new Rectangle(1 + nesting, (int)location.Y + diff, Size.Width- nesting, height + (diff / 2)), FormDesign.Design.ActiveColor.MergeColor(FormDesign.Design.AccentBackColor, 35 - nesting / 2), FormDesign.Design.ActiveColor.MergeColor(FormDesign.Design.AccentBackColor, 5), 0f);

			if (nesting > 0) 
				DrawQuote(height, (int)activeFormats.Last(x => x.Format is ForumFormat.QUOTE).Options["Nesting"] - 1);

			e.Graphics.FillRectangle(brush2, new Rectangle(1 + nesting, (int)location.Y + diff, Size.Width - nesting, height + (diff / 2) + 1));
			e.Graphics.FillRectangle(brush1, new Rectangle(0 + nesting, (int)location.Y + diff, (int)(3 * UI.FontScale), height + (diff / 2) + 1));
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
}
