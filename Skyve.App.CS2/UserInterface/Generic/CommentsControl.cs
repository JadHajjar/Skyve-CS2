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
public partial class CommentsControl : UserControl
{
	private IModCommentsInfo? modCommentsInfo;

	public IPackageIdentity? Package { get; set; }

    public CommentsControl()
	{
		InitializeComponent();
	}

	protected override async void OnCreateControl()
	{
		base.OnCreateControl();

		if (Package is null)
			return;

		modCommentsInfo = await ServiceCenter.Get<IWorkshopService>().GetComments(Package);
	}

	private void C_Comments_Paint(object sender, PaintEventArgs e)
	{
		if(modCommentsInfo?.Posts is null) return;

		var y = 0;
		foreach (var item in modCommentsInfo.Posts)
		{
			e.Graphics.DrawStringItem(item.Message, Font, ForeColor, C_Comments.ClientRectangle, ref y);
		}

		C_Comments.Height = y;
	}
}
