﻿using Mono.Cecil;

using System;
using System.Collections.Generic;

namespace Skyve.Systems.CS1.Utilities.IO;
internal class AssemblyResolver : BaseAssemblyResolver
{
	private readonly IDictionary<string, AssemblyDefinition> cache
		= new Dictionary<string, AssemblyDefinition>(StringComparer.Ordinal);

	private ReaderParameters? readerParameters_;
	public ReaderParameters ReaderParameters
	{
		get => readerParameters_ ?? new ReaderParameters();
		set => readerParameters_ = value;
	}

	public override AssemblyDefinition Resolve(AssemblyNameReference name)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}

		return cache.TryGetValue(name.FullName, out var assembly) ? assembly : (cache[name.FullName] = Resolve(name, ReaderParameters));
	}

	protected void RegisterAssembly(AssemblyDefinition assembly)
	{
		if (assembly == null)
		{
			throw new ArgumentNullException("assembly");
		}

		var name = assembly.Name.FullName;
		if (!cache.ContainsKey(name))
		{
			cache[name] = assembly;
		}
	}

	protected override void Dispose(bool disposing)
	{
		foreach (var assemblyDefinition in cache.Values)
		{
			assemblyDefinition.Dispose();
		}

		cache.Clear();
		base.Dispose(disposing);
	}
}
