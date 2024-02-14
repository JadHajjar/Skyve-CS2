using Skyve.Domain;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Service.CS2.Systems;
internal class InterfaceService : IInterfaceService
{
	public void OpenOptionsPage()
	{
	}

	public void OpenPackagePage(IPackageIdentity package, bool openCompatibilityPage)
	{
	}

	public void OpenParadoxLogin()
	{
	}

	public void ViewSpecificPackages(List<IPackageIdentity> packages, string title)
	{
	}
}
