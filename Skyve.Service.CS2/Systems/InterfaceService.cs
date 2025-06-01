using Skyve.Domain;
using Skyve.Domain.Systems;

using System.Collections.Generic;

namespace Skyve.Service.CS2.Systems;
internal class InterfaceService : IInterfaceService
{
	public bool AskForDependencyConfirmation(List<IPackageIdentity> packages, List<IPackageIdentity> dependencies)
	{
		return false;
	}

	public void OpenLogReport(bool save)
	{
	}

	public void OpenOptionsPage()
	{
	}

	public void OpenPackagePage(IPackageIdentity package, bool openCompatibilityPage, bool openCommentsPage)
	{
	}

	public void OpenParadoxLogin()
	{
	}

	public void OpenPlaysetPage(IPlayset playset, bool settingsTab = false)
	{
	}

	public void OpenPlaysetPage(IPlayset playset, bool settingsTab = false, bool editName = false)
	{
	}

	public void RestoreBackup(string restoreBackup)
	{
	}

	public void ViewSpecificPackages(List<IPackageIdentity> packages, string title)
	{
	}
}
