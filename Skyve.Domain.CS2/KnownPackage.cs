﻿using System;

namespace Skyve.Domain.CS2;
public class KnownPackage
{
	public KnownPackage(ILocalPackageData x)
	{
		Folder = x.Folder;
		UpdateTime = x.LocalTime;
	}

	public KnownPackage()
	{

	}

	public string? Folder { get; set; }
	public DateTime UpdateTime { get; set; }
}
