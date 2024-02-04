using System.Reflection;

namespace Skyve.App.CS2.UserInterface.Panels;
internal class PC_SkyveChangeLog : PC_Changelog
{
	public PC_SkyveChangeLog() : base(
		Assembly.GetExecutingAssembly(),
		$"Skyve.App.CS2.Changelog.json",
		Assembly.GetExecutingAssembly().GetName().Version)
	{
		LocaleChangelog.Load();
	}

	protected override void PrepareChangelog(List<VersionChangeLog> changeLogs)
	{
#if Stable
		changeLogs.RemoveAll(x => x.Beta);
#else
		changeLogs.RemoveAll(x => x.Stable);
#endif
		if (System.Diagnostics.Debugger.IsAttached)
		{
			var current = changeLogs.First();

			System.Windows.Forms.Clipboard.SetText($"# :skyve: Skyve v{current.VersionString}{(current.Stable ? " [Stable]" : "")}{(current.Beta ? " [Beta]" : "")}\r\n"
				+ (string.IsNullOrEmpty(current.Tagline) ? string.Empty : $"### *{current.Tagline}*\r\n")
				+ current.ChangeGroups.ListStrings(x => $"## {x.Name}\r\n{x.Changes.ListStrings(y => $"* {y}", "\r\n")}", "\r\n\r\n"));
		}
	}
}
