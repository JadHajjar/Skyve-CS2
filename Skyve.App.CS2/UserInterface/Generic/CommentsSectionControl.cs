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
public partial class CommentsSectionControl : UserControl
{
	private IModCommentsInfo? modCommentsInfo;

	public IPackageIdentity? Package { get; set; }

    public CommentsSectionControl()
	{
		InitializeComponent();
	}

	protected override async void OnCreateControl()
	{
		base.OnCreateControl();

		if (Package is null)
			return;

		modCommentsInfo = await ServiceCenter.Get<IWorkshopService>().GetComments(Package);

		if (modCommentsInfo?.Posts == null) return;

		foreach (var item in modCommentsInfo.Posts)
		{
			var control = new CommentControl(item, Package);

			Controls.Add(control);
			Controls.SetChildIndex(control, 0);
		}
	}
}
