using System.Drawing;
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

		L_Author.Text = _comment.Username;
		L_Time.Text = _comment.Created.ToLocalTime().ToRelatedString();
		L_AuthorLabel.Visible = _comment.Username == packageIdentity.GetWorkshopInfo()?.Author?.Id?.ToString();
	}

	protected override void UIChanged()
	{
		TLP_Back.Padding =Padding= UI.Scale(new Padding(6), UI.FontScale);
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
		e.Graphics.DrawString(_comment.Message, Font, new SolidBrush(ForeColor), C_Message.ClientRectangle);

		C_Message.Height = (int)e.Graphics.Measure(_comment.Message, Font, C_Message.ClientRectangle.Width).Height;
	}

	private void C_UserImage_Paint(object sender, PaintEventArgs e)
	{

	}
}
