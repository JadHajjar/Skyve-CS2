﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyve.Domain.CS2.Utilities;
public class ModStateCollection
{
	private readonly Dictionary<int, Dictionary<ulong, bool>> _enabledConfig;
	private readonly Dictionary<int, Dictionary<string, bool>> _enabledLocalConfig;
	private readonly Dictionary<int, Dictionary<ulong, string>> _versionConfig;

	public ModStateCollection()
	{
		_enabledConfig = [];
		_enabledLocalConfig = [];
		_versionConfig = [];
	}

	public ModStateCollection(Dictionary<int, Dictionary<ulong, bool>> enabledConfig, Dictionary<int, Dictionary<string, bool>> enabledLocalConfig, Dictionary<int, Dictionary<ulong, string>> versionConfig)
	{
		_enabledConfig = enabledConfig;
		_enabledLocalConfig = enabledLocalConfig;
		_versionConfig = versionConfig;
	}

	public bool IsEmpty => _enabledConfig.Count == 0 && _versionConfig.Count == 0;

	public bool IsIncluded(int playset, ulong modId, string? version)
	{
		if (!_enabledConfig.TryGetValue(playset, out var dic)
			|| !dic.ContainsKey(modId))
		{
			return false;
		}

		if (version is null or "")
		{
			return true;
		}

		return _versionConfig.TryGetValue(playset, out var versionDic)
			&& versionDic.TryGetValue(modId, out var ver)
			&& (ver == version || ver == "");
	}

	public bool IsIncluded(int playset, string modName)
	{
		return _enabledLocalConfig.TryGetValue(playset, out var dic)
			&& dic.ContainsKey(modName);
	}

	public void Remove(int playset, ulong modId)
	{
		if (_enabledConfig.TryGetValue(playset, out var dic1))
		{
			dic1.Remove(modId);
		}

		if (_versionConfig.TryGetValue(playset, out var dic2))
		{
			dic2.Remove(modId);
		}
	}

	public void Remove(int playset, string modName)
	{
		if (_enabledLocalConfig.TryGetValue(playset, out var dic1))
		{
			dic1.Remove(modName);
		}
	}

	public bool IsEnabled(int playset, ulong modId, string? version)
	{
		if (!_enabledConfig.TryGetValue(playset, out var dic)
			|| !dic.TryGetValue(modId, out var enabled)
			|| !enabled)
		{
			return false;
		}

		if (version is null or "")
		{
			return true;
		}

		return _versionConfig.TryGetValue(playset, out var versionDic)
			&& versionDic.TryGetValue(modId, out var ver)
			&& (ver == version || ver == "");
	}

	public bool IsEnabled(int playset, string modName)
	{
		return _enabledLocalConfig.TryGetValue(playset, out var dic)
			&& dic.TryGetValue(modName, out var enabled)
			&& enabled;
	}

	public void SetEnabled(int playset, ulong modId, bool enabled)
	{
		if (_enabledConfig.TryGetValue(playset, out var dic))
		{
			dic[modId] = enabled;
		}
		else
		{
			_enabledConfig[playset] = new() { [modId] = enabled };
		}
	}

	public void SetEnabled(int playset, string name, bool enabled)
	{
		if (_enabledLocalConfig.TryGetValue(playset, out var dic))
		{
			dic[name] = enabled;
		}
		else
		{
			_enabledLocalConfig[playset] = new() { [name] = enabled };
		}
	}

	public string? GetVersion(int playset, ulong modId)
	{
		if (_versionConfig.TryGetValue(playset, out var dic) && dic.TryGetValue(modId, out var version))
		{
			return version;
		}

		return null;
	}

	public IEnumerable<int> GetIncludedPlaysets(ulong modId, string? version, bool andEnabled)
	{
		foreach (var item in _enabledConfig)
		{
			if (!_enabledConfig.TryGetValue(item.Key, out var dic)
				|| !dic.TryGetValue(modId, out var enabled)
				|| (!enabled && andEnabled))
			{
				continue;
			}

			if (version is not null and not "")
			{
				if (!_versionConfig.TryGetValue(item.Key, out var versionDic)
					|| !versionDic.TryGetValue(modId, out var ver)
					|| (ver != version && ver != ""))
				{
					continue;
				}
			}

			yield return item.Key;
		}
	}

	public void SetVersion(int playset, ulong modId, string version)
	{
		if (_versionConfig.TryGetValue(playset, out var dic))
		{
			dic[modId] = version;
		}
		else
		{
			_versionConfig[playset] = new() { [modId] = version };
		}
	}

	public void SetState(int playset, IPackageIdentity mod, bool enabled, string version)
	{
		if (mod.Id == 0)
		{
			SetEnabled(playset, mod.Name, enabled);
		}
		else
		{
			SetEnabled(playset, mod.Id, enabled);
			SetVersion(playset, mod.Id, version);
		}
	}

	public ModStateCollection Clone()
	{
		return new ModStateCollection(
			_enabledConfig.ToDictionary(x => x.Key, y => new Dictionary<ulong, bool>(y.Value)),
			_enabledLocalConfig.ToDictionary(x => x.Key, y => new Dictionary<string, bool>(y.Value)),
			_versionConfig.ToDictionary(x => x.Key, y => new Dictionary<ulong, string>(y.Value))
		);
	}

	public Dictionary<int, Dictionary<ulong, (bool IsEnabled, string? Version)>> ToDictionary()
	{
		var finalDic = new Dictionary<int, Dictionary<ulong, (bool, string?)>>();

		foreach (var playsetId in _enabledConfig.Keys)
		{
			var enabledDict = _enabledConfig[playsetId];
			var versionDict = _versionConfig[playsetId];
			var combinedDict = new Dictionary<ulong, (bool IsEnabled, string? Version)>();

			foreach (var modId in enabledDict.Keys)
			{
				combinedDict[modId] = (enabledDict[modId], versionDict.TryGetValue(modId, out var version) ? version : null);
			}

			finalDic[playsetId] = combinedDict;
		}

		return finalDic;
	}

	//public Fragment CreateFragment(int playsetId)
	//{
	//	return new Fragment(playsetId
	//		, _enabledConfig.TryGetValue(playsetId, out var enabledConfig) ? enabledConfig : []
	//		, _versionConfig.TryGetValue(playsetId, out var versionConfig) ? versionConfig : []);
	//}

	//public class Fragment
	//{
	//	private readonly Dictionary<ulong, bool> _enabledConfig;
	//	private readonly Dictionary<ulong, string> _versionConfig;

	//	public int PlaysetId { get; }

	//	public Fragment(int playsetId, Dictionary<ulong, bool> enabledConfig, Dictionary<ulong, string> versionConfig)
	//	{
	//		PlaysetId = playsetId;
	//		_enabledConfig = new(enabledConfig);
	//		_versionConfig = new(versionConfig);
	//	}
	//}

	public override bool Equals(object obj)
	{
		if (obj is not ModStateCollection modStateCollection)
		{
			return false;
		}

		if (_enabledConfig.Keys.SequenceEqual(modStateCollection._enabledConfig.Keys))
		{
			return false;
		}

		foreach (var key in _enabledConfig.Keys)
		{
			if (!_enabledConfig.ContainsKey(key))
			{
				if (modStateCollection._enabledConfig.ContainsKey(key))
				{
					return false;
				}

				continue;
			}

			if (!modStateCollection._enabledConfig.ContainsKey(key))
			{
				return false;
			}

			if (!_enabledConfig[key].SequenceEqual(modStateCollection._enabledConfig[key]))
			{
				return false;
			}
		}

		return true;
	}

	public override int GetHashCode()
	{
		var hashCode = 19070429;
		hashCode = (hashCode * -1521134295) + EqualityComparer<Dictionary<int, Dictionary<ulong, bool>>>.Default.GetHashCode(_enabledConfig);
		hashCode = (hashCode * -1521134295) + EqualityComparer<Dictionary<int, Dictionary<ulong, string>>>.Default.GetHashCode(_versionConfig);
		return hashCode;
	}
}
