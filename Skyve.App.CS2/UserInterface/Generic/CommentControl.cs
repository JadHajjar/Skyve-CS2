using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
public partial class CommentControl : UserControl
{
	private readonly IModComment _comment;
	private readonly IPackageIdentity _packageIdentity;

	public CommentControl(IModComment comment, IPackageIdentity		packageIdentity)
	{
		InitializeComponent();
		_comment = comment;
		_packageIdentity = packageIdentity;

		L_Author.Text = _comment.UserTitle;
		L_Time.Text = _comment.Created.ToLocalTime().ToRelatedString();
		L_AuthorLabel.Visible = _comment.Username == packageIdentity.GetWorkshopInfo()?.Author?.Id?.ToString();

		roundedTableLayoutPanel1.Padding = UI.Scale(new Padding(6), UI.FontScale);
	}
}
