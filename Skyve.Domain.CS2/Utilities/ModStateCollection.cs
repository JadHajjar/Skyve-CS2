using Skyve.Systems;

using System.Collections.Generic;

namespace Skyve.Domain.CS2.Utilities;

public class ModStateCollection
{
	private readonly Dictionary<string, Dictionary<string, bool>> _enabledConfig;
	private readonly Dictionary<string, Dictionary<string, string>> _versionConfig;

	public ModStateCollection()
	{
		_enabledConfig = [];
		_versionConfig = [];
	}

	public ModStateCollection(Dictionary<string, Dictionary<string, bool>> enabledConfig, Dictionary<string, Dictionary<string, string>> versionConfig)
	{
		_enabledConfig = enabledConfig;
		_versionConfig = versionConfig;
	}

	public bool IsEmpty => _enabledConfig.Count == 0 && _versionConfig.Count == 0;

	public bool IsIncluded(string? playset, IPackageIdentity package, bool withVersion = true)
	{
		var key = $"{package.Source}_{package.Id}";

		if (playset is null
			|| !_enabledConfig.TryGetValue(playset, out var dic)
			|| !dic.ContainsKey(key))
		{
			return false;
		}

		if (!withVersion || package.Version is null or "")
		{
			return true;
		}

		return _versionConfig.TryGetValue(playset, out var versionDic)
			&& versionDic.TryGetValue(key, out var ver)
			&& (ver == package.Version || ver == "");
	}

	public void Remove(string? playset, IPackageIdentity package)
	{
		if (playset is null)
			return;

		var key = $"{package.Source}_{package.Id}";

		if (_enabledConfig.TryGetValue(playset, out var dic1))
		{
			dic1.Remove(key);
		}

		if (_versionConfig.TryGetValue(playset, out var dic2))
		{
			dic2.Remove(key);
		}
	}

	public bool IsEnabled(string? playset, IPackageIdentity package, bool withVersion = true)
	{
		var key = $"{package.Source}_{package.Id}";

		if (playset is null
			|| !_enabledConfig.TryGetValue(playset, out var dic)
			|| !dic.TryGetValue(key, out var enabled)
			|| !enabled)
		{
			return false;
		}

		if (!withVersion || package.Version is null or "")
		{
			return true;
		}

		return _versionConfig.TryGetValue(playset, out var versionDic)
			&& versionDic.TryGetValue(key, out var ver)
			&& (ver == package.Version || ver == "");
	}

	public void SetEnabled(string? playset, IPackageIdentity package, bool enabled)
	{
		if (playset is null)
			return;

		var key = $"{package.Source}_{package.Id}";

		if (_enabledConfig.TryGetValue(playset, out var dic))
		{
			dic[key] = enabled;
		}
		else
		{
			_enabledConfig[playset] = new() { [key] = enabled };
		}
	}

	public string? GetVersion(string? playset, IPackageIdentity package)
	{
		if (playset is not null && _versionConfig.TryGetValue(playset, out var dic) && dic.TryGetValue($"{package.Source}_{package.Id}", out var version))
		{
			return version;
		}

		return null;
	}

	public IEnumerable<string> GetIncludedPlaysets(IPackageIdentity package, bool andEnabled, bool withVersion = true)
	{
		var key = $"{package.Source}_{package.Id}";
		foreach (var item in _enabledConfig)
		{
			if (!_enabledConfig.TryGetValue(item.Key, out var dic)
				|| !dic.TryGetValue(key, out var enabled)
				|| (!enabled && andEnabled))
			{
				continue;
			}

			if (!withVersion || package.Version is not null and not "")
			{
				if (!_versionConfig.TryGetValue(item.Key, out var versionDic)
					|| !versionDic.TryGetValue(key, out var ver)
					|| (ver != package.Version && ver != ""))
				{
					continue;
				}
			}

			yield return item.Key;
		}
	}

	public void SetVersion(string? playset, IPackageIdentity package)
	{
		if (playset is null)
			return;

		var key = $"{package.Source}_{package.Id}";

		if (_versionConfig.TryGetValue(playset, out var dic))
		{
			if (package.Version is null)
			{
				dic.Remove(key);
			}
			else
			{
				dic[key] = package.Version;
			}
		}
		else if (package.Version is not null)
		{
			_versionConfig[playset] = new() { [key] = package.Version };
		}
	}

	public void SetState(string? playset, IPackageIdentity mod, bool enabled)
	{
		SetEnabled(playset, mod, enabled);
		SetVersion(playset, mod);
	}
}
