using System;
using System.Collections.Generic;
using System.IO;

namespace Skyve.Domain.CS2.Utilities;
public class ModCollection
{
	private readonly Dictionary<string, List<IPackage>> _modList = new(StringComparer.OrdinalIgnoreCase);
	private readonly Dictionary<string, CollectionInfo> _collectionList;

	public ModCollection(Dictionary<string, CollectionInfo> collectionList)
	{
		_collectionList = collectionList;
	}

	public void AddMod(IPackage mod)
	{
		var key = Path.GetFileName(mod.LocalData?.FilePath);

		if (_modList.ContainsKey(key))
		{
			_modList[key].Add(mod);
		}
		else
		{
			_modList.Add(key, [mod]);
		}
	}

	public void RemoveMod(IPackage mod)
	{
		var key = Path.GetFileName(mod.LocalData?.FilePath);

		if (_modList.ContainsKey(key))
		{
			_modList[key].Remove(mod);
		}
	}

	public List<IPackage>? GetCollection(IPackage mod, out CollectionInfo? collection)
	{
		var key = Path.GetFileName(mod.LocalData?.FilePath);

		return GetCollection(key, out collection);
	}

	public List<IPackage>? GetCollection(string key, out CollectionInfo? collection)
	{
		if (_modList.ContainsKey(key))
		{
			collection = _collectionList[key];

			return _modList[key];
		}

		collection = null;
		return null;
	}

	public void CheckAndAdd(IPackage mod)
	{
		var fileName = Path.GetFileName(mod.LocalData?.FilePath);

		if (_collectionList.ContainsKey(fileName))
		{
			AddMod(mod);
		}
	}

	public IEnumerable<List<IPackage>> Collections => _modList.Values;
}

public class CollectionInfo
{
	public bool Required { get; set; }
	public bool Forbidden { get; set; }
}
