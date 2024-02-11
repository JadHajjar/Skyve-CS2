using Extensions;

using Skyve.Mod.CS2.Shared;

using System.Collections.Generic;

namespace Skyve.Mod.CS2
{
	public class Locale : LocaleHelper
	{
		private static readonly Locale _instance = new Locale();

		public static IEnumerable<DictionarySource> GetAvailableSources() => _instance.GetAvailableLanguages();

		public Locale() : base($"Skyve.Mod.CS2.Properties.LocaleModCS2.json") { }
	}
}