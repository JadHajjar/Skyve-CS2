using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.Enums;

namespace Skyve.Domain.CS2.Paradox;
public class ParadoxLink : ILink
{
	public LinkType Type { get; set; }
	public string? Url { get; set; }
	public string? Title { get; set; }

	public ParadoxLink(ExternalLink externalLink)
	{
		Url = externalLink.URL;
		Title = Type.ToString();
		Type = externalLink.Type switch
		{
			"discord" => LinkType.Discord,
			"github" => LinkType.Github,
			"youtube" => LinkType.YouTube,
			"twitch" => LinkType.Twitch,
			"x" => LinkType.X,
			"paypal" => LinkType.Paypal,
			"patreon" => LinkType.Patreon,
			"buymeacoffee" => LinkType.BuyMeACoffee,
			"crowdin" => LinkType.Crowdin,
			"kofi" => LinkType.Kofi,
			"gitlab" => LinkType.Gitlabs,
			_ => LinkType.Website
		};

		if (Type is LinkType.BuyMeACoffee)
		{
			Title = "Buy Me A Coffee";
		}
	}

	public ParadoxLink()
	{

	}
}
