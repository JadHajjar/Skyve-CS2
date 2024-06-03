using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
public partial class CommentsSectionControl : UserControl
{
	private IModCommentsInfo? modCommentsInfo;
	private bool isLoading;
	private int page = 1;

	public IPackageIdentity? Package { get; set; }

	public CommentsSectionControl()
	{
		InitializeComponent();

		AutoSize = true;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		PB_Loading.Size = UI.Scale(new System.Drawing.Size(24, 24), UI.FontScale);
		PB_Loading.Location = new System.Drawing.Point((Width - PB_Loading.Width) / 2, (int)(175 * UI.FontScale));

		this.TryBeginInvoke(FindScroll);

		Task.Run(() => LoadComments());
	}

	private void FindScroll()
	{
		var scrollbar = SlickScroll.GlobalGetScrollbar(this);

		if (scrollbar != null)
		{
			scrollbar.Scroll += Scrollbar_Scroll;
		}
	}

	private void Scrollbar_Scroll(object sender, ScrollEventArgs e)
	{
		if (e.NewValue > 90)
		{
			Task.Run(() => LoadComments(page + 1));
		}
	}

	private async void LoadComments(int page = 1)
	{
		if (Package is null || isLoading)
		{
			return;
		}

		isLoading = true;

		modCommentsInfo = await ServiceCenter.Get<IWorkshopService>().GetComments(Package, page);

		if ((modCommentsInfo?.Posts) != null)
		{
			this.page = page;

			this.TryInvoke(() =>
			{
				PB_Loading.Visible = false;

				this.SuspendDrawing();
				foreach (var item in modCommentsInfo.Posts)
				{
					var control = new CommentControl(item, Package) { Dock = DockStyle.Top };

					Controls.Add(control);
					Controls.SetChildIndex(control, 0);
				}
				this.ResumeDrawing();
			});
		}

		isLoading = false;
	}
}
