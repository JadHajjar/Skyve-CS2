using Skyve.App.CS2.UserInterface.Panels;

using System.Drawing;
using System.Reflection;

namespace Skyve.App.CS2;
internal class LastVersionNotification : INotificationInfo
{
	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color { get; }
	public bool HasAction { get; }

	public LastVersionNotification()
	{
		var changelogs = PC_Changelog.GetChangelogs(Assembly.GetEntryAssembly(), $"Skyve.App.CS2.Changelog.json");
		var currentVersion = Assembly.GetEntryAssembly().GetName().Version;
		var currentChangelog = changelogs.FirstOrDefault(x => x.Version.Major == currentVersion.Major && x.Version.Minor == currentVersion.Minor && x.Version.Build == currentVersion.Build && Math.Max(0, x.Version.Revision) == Math.Max(0, currentVersion.Revision));

		if (currentChangelog is null)
		{
			Title = $"v{currentVersion.GetString()} Update";
			Icon = "I_Question";
			return;
		}

		Title = $"v{currentVersion.GetString()} Update";
		Description = LocaleHelper.GetGlobalText(currentChangelog.Tagline.IfEmpty(currentChangelog.ChangeGroups[0].Name));
		Icon = "I_Versions";
		Time = currentChangelog.Date ?? DateTime.Now;
		HasAction = true;
	}

	public void OnClick()
	{
		App.Program.MainForm.PushPanel<PC_SkyveChangeLog>();
	}

	public void OnRightClick()
	{
	}
}
