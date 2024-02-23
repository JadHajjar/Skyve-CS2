using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Paradox;
public class ParadoxLink : ILink
{
	public LinkType Type { get; set; }
	public string? Url { get; set; }
	public string? Title { get; set; }

	public ParadoxLink(ExternalLink externalLink)
    {
		Url = externalLink.URL;
		Title = externalLink.Type;

		Enum.TryParse(externalLink.Type, out LinkType type);

		Type = type;
	}

    public ParadoxLink()
    {
        
    }
}
