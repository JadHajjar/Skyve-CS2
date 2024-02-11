using PDX.SDK.Contracts.Service.Mods.Models;

using System;

namespace Skyve.Domain.CS2.Paradox;
public class ModChangelog : IModChangelog
{
    public string? VersionId { get; set; }
    public string? Version { get; set; }
	public DateTime? ReleasedDate { get; set; }
	public string? Details { get; set; }

	public ModChangelog(Change change)
	{
		VersionId = change.Version;
		Version = change.UserModVersion;
		ReleasedDate = change.ReleasedDate;
		Details = change.Details;
	}

    public ModChangelog()
    {
        
    }
}