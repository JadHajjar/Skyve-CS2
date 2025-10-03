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

		Title = Type.ToString();

		if (Type is LinkType.BuyMeACoffee)
		{
			Title = "Buy Me A Coffee";
		}
	}

	public ParadoxLink()
	{

	}

	public static string ToParadoxLinkType(LinkType linkType)
	{
		return linkType switch
		{
			LinkType.Discord => "discord",
			LinkType.Github => "github",
			LinkType.YouTube => "youtube",
			LinkType.Twitch => "twitch",
			LinkType.X => "x",
			LinkType.Paypal => "paypal",
			LinkType.Patreon => "patreon",
			LinkType.BuyMeACoffee => "buymeacoffee",
			LinkType.Crowdin => "crowdin",
			LinkType.Kofi => "kofi",
			LinkType.Gitlabs => "gitlab",
			_ => ""
		};
	}
}
