﻿using SlickControls.Controls.Advanced;

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
public partial class PackageChangelogControl : SlickControl
{
	private readonly PanelItemControl base_P_Tabs;
	private IModChangelog[]? _modChangelogs;
	private IModChangelog? _current;

	public PackageChangelogControl()
	{
		InitializeComponent();

		base_P_Tabs = new PanelItemControl(null) { Dock = DockStyle.Fill };
		roundedPanel1.Controls.Add(base_P_Tabs);
	}

	protected override void UIChanged()
	{
		roundedPanel1.Padding = UI.Scale(new Padding(5));
		roundedPanel1.Width = UI.Scale(150);
		Padding = UI.Scale(new Padding(5));
	}

	protected override void DesignChanged(FormDesign design)
	{
		roundedPanel1.BackColor = design.BackColor.Tint(Lum: design.IsDarkTheme ? 6 : -6);

		slickWebBrowser1.Head = $@"
<style>
	.version {{
		padding-left: {5 * UI.FontScale:0}px;
		margin-bottom: {32 * UI.FontScale:0}px;
	}}

	.version .log {{
		padding-left: {10 * UI.FontScale:0}px;
		margin: {10 * UI.FontScale:0}px;
		border-left: {2 * UI.FontScale:0}px solid {SlickWebBrowser.RGB(FormDesign.Design.AccentColor)};
	}}

	.version .header {{
		font-size: {16 * UI.FontScale:0.00}pt;
		font-weight: 900;
		padding-bottom: {10 * UI.FontScale:0}px;
		color: {SlickWebBrowser.RGB(FormDesign.Design.ForeColor)};
	}}

	.version .date {{
		margin-left: {6 * UI.FontScale:0}px;
		font-size: {7.5 * UI.FontScale:0.00}pt;
		color: {SlickWebBrowser.RGB(FormDesign.Design.InfoColor)};
	}}

	h1, h2, h3 {{
		border-bottom: none;
		padding-bottom: 0;
	}}
</style>";
	}

	public void SetChangelogs(string currentVersionId, IEnumerable<IModChangelog> changelogs)
	{
		_modChangelogs = changelogs.ToArray();
		_current = _modChangelogs.FirstOrDefault(x => x.VersionId == currentVersionId);

		base_P_Tabs.Clear();

		if (_current != null)
		{
			base_P_Tabs.Add(PanelTab.GroupName("Current Version"));
			AddVersion(_current, $"v{_current.Version}");
		}

		var addedTitle = false;

		foreach (var grp in _modChangelogs
			.Where(x => _current == null || x != _current)
			.Distinct((x, y) => Major(x.Version, out _) == Major(y.Version, out _) && Minor(x.Version, out _) == Minor(y.Version, out _))
			.OrderByDescending(x => x.ReleasedDate)
			.GroupBy(x => Major(x.Version, out _)))
		{
			foreach (var item in grp)
			{
				if (!addedTitle)
				{
					base_P_Tabs.Add(PanelTab.Separator());
					base_P_Tabs.Add(PanelTab.GroupName("All Versions"));
					addedTitle = true;
				}

				AddVersion(item);
			}
		}
	}

	private void AddVersion(IModChangelog versionInfo, string? text = null)
	{
		var M = Major(versionInfo.Version, out _);
		var m = Minor(versionInfo.Version, out _);
		var vers = _modChangelogs
			.Where(x => Major(x.Version, out _) == M && Minor(x.Version, out _) == m)
			.OrderBy(x => x.ReleasedDate);

		var firstVersion = Regex.Match(vers.First().Version, @"^[^\.]+\.[^\.]+\.?[^\.]*\.?").Value.Trim('.');
		var lastVersion = Regex.Match(vers.Last().Version, @"^[^\.]+\.[^\.]+\.?[^\.]*\.?").Value.Trim('.');

		var tab = new PanelTab(new PanelItem()
		{
			Text = text.IfNull(vers.Count() == 1 ? $"v {versionInfo.Version}" : $"v {firstVersion} → {lastVersion}"),
			Data = text != null ? null : versionInfo
		});

		tab.PanelItem.OnClick += Tile_Click;

		base_P_Tabs.Add(tab);

		if (text != null)
		{
			Tile_Click(tab.PanelItem, null);
		}
	}

	private void Tile_Click(object sender, MouseEventArgs? e)
	{
		var inf = (IModChangelog)(sender as PanelItem)!.Data;
		var changelogs = new List<IModChangelog>();

		if (inf == null)
		{
			if (_current != null)
			{
				changelogs.Add(_current);
			}
		}
		else
		{
			foreach (var item in _modChangelogs.Where(x => Major(x.Version, out _) == Major(inf.Version, out _) && Minor(x.Version, out _) == Minor(inf.Version, out _)).OrderByDescending(x => x.ReleasedDate))
			{
				changelogs.Add(item);
			}
		}

		var html = new StringBuilder();

		foreach (var item in changelogs)
		{
			var log = item.Details.RegexRemove($@"# (v\d|{item.Version}).*");

			html.AppendLine("<div class=\"version\">");
			html.AppendLine($"<span class=\"header\">v{item.Version}</span> <span class=\"date\">{item.ReleasedDate?.ToLocalTime():g}</span> <div class=\"log\">");
			html.AppendLine(Markdig.Markdown.ToHtml(log));
			html.AppendLine("</div></div>");
		}

		slickWebBrowser1.Body = html.ToString();

		base_P_Tabs.Items.Where(x => x.PanelItem != null).Foreach(x => x.PanelItem.Selected = x.PanelItem == sender);
		base_P_Tabs.Invalidate();
	}

	private string Major(string? version, out string full)
	{
		if (version is null or "")
		{
			return full = string.Empty;
		}

		var regex = Regex.Match(version, @"^([^\.]+)\.");

		if (regex.Success)
		{
			full = regex.Groups[1].Value;

			return Regex.Match(regex.Groups[1].Value, @"^\d+").Value.IfEmpty(full);
		}

		return full = string.Empty;
	}

	private string Minor(string? version, out string full)
	{
		if (version is null or "")
		{
			return full = string.Empty;
		}

		var regex = Regex.Match(version, @"^[^\.]+\.([^\.]+)\.?");

		if (regex.Success)
		{
			full = regex.Groups[1].Value;

			return Regex.Match(regex.Groups[1].Value, @"^\d+").Value.IfEmpty(full);
		}

		return full = string.Empty;
	}

	private string Build(string? version, out string full)
	{
		if (version is null or "")
		{
			return full = string.Empty;
		}

		var regex = Regex.Match(version, @"^[^\.]+\.[^\.]+\.([^\.]+)\.?");

		if (regex.Success)
		{
			full = regex.Groups[1].Value;

			return Regex.Match(regex.Groups[1].Value, @"^\d+").Value.IfEmpty(full);
		}

		return full = string.Empty;
	}
}
