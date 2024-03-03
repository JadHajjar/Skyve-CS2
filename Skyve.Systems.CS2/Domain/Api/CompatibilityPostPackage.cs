using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Domain.Api;
public class CompatibilityPostPackage : PackageData
{
	public bool IsBlackListedById { get; set; }
	public bool IsBlackListedByName { get; set; }
}
