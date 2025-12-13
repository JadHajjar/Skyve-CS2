using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Panels;
using Skyve.Systems.CS2.Utilities;

using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_CompatibilityBulkEditing : PanelContent
{
	public PC_CompatibilityBulkEditing()
	{
		InitializeComponent();
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		slickSpacer1.Height = UI.Scale(1);
		foreach (Control item in tableLayoutPanel1.Controls)
		{
			item.Margin = UI.Scale(new Padding(5));
		}
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		slickSpacer1.BackColor = design.AccentColor;
	}

	private void B_Add_Click(object sender, EventArgs e)
	{
		var form = new PC_WorkshopPackageSelection();

		form.PackageSelected += OnPackageSelected;

		Form.PushPanel(form);
	}

	private void B_Paste_Click(object sender, EventArgs e)
	{
		if (!Clipboard.ContainsText())
		{
			return;
		}

		var matches = Regex.Matches(Clipboard.GetText(), @"(?<!\.)\b(\d{5,6})\b(?:\: (.+?) (?:[\d\.]))?");
		var packages = new List<ulong>();

		foreach (Match item in matches)
		{
			if (ulong.TryParse(item.Groups[1].Value, out var id))
			{
				packages.Add(id);
			}
		}

		OnPackageSelected(packages);
	}

	private void OnPackageSelected(IEnumerable<ulong> enumerable)
	{
		foreach (var id in enumerable)
		{
			if (!smartFlowPanel1.Controls.Any(x => (x as MiniPackageControl)!.Package.Id == id))
			{
				smartFlowPanel1.Controls.Add(new MiniPackageControl(id) { Dock = DockStyle.Top });
			}
		}
	}

	private async void B_Apply_Click(object sender, EventArgs e)
	{
		if (ShowPrompt($"This will mark all {smartFlowPanel1.Controls.Count} mods as broken from patch {ServiceCenter.Get<ICitiesManager>().GameVersion}.\r\n\r\nAre you sure you want to proceed?", PromptButtons.YesNo, PromptIcons.Hand) == DialogResult.No)
		{
			return;
		}

		B_Apply.Enabled = false;
		B_Apply.Loading = true;

		var response = await ServiceCenter.Get<SkyveApiUtil>().BulkUpdatePackageData(new()
		{
			Packages = smartFlowPanel1.Controls.Cast<MiniPackageControl>().ToList(x => x.Package.Id),
			ReviewedGameVersion = ServiceCenter.Get<ICitiesManager>().GameVersion,
			Stability = Compatibility.Domain.Enums.PackageStability.BrokenFromPatch
		});

		B_Apply.Enabled = true;
		B_Apply.Loading = false;

		if (!response.Success)
		{
			ShowPrompt(response.Message, PromptButtons.OK, PromptIcons.Error);
			return;
		}

		PushBack();
	}
}
