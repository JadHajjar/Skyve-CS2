using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.App.CS2;
internal class InstallerUtil
{
	public void Install()
	{

	}

	private static void CreateUninstaller()
	{
		using (var parent = Registry.LocalMachine.OpenSubKey(
					 @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
		{
			if (parent == null)
			{
				throw new Exception("Uninstall registry key not found.");
			}
			try
			{
				RegistryKey key = null;

				try
				{
					var GUID = Guid.Parse("{8E2BF133-6358-F7AB-3289-18140365A644}");
					var guidText = GUID.ToString("B");
					key = parent.OpenSubKey(guidText, true) ??
						  parent.CreateSubKey(guidText);

					if (key == null)
					{
						throw new Exception(string.Format("Unable to create uninstaller '{0}'", guidText));
					}

					var asm = Assembly.GetExecutingAssembly();
					var v = asm.GetName().Version;
					var exe = "\"" + asm.CodeBase.Substring(8).Replace("/", "\\\\") + "\"";

					key.SetValue("DisplayName", "Skyve");
					key.SetValue("ApplicationVersion", v.ToString());
					key.SetValue("Publisher", "Slick Apps");
					key.SetValue("DisplayIcon", exe);
					key.SetValue("DisplayVersion", v.ToString(2));
					key.SetValue("URLInfoAbout", "");
					key.SetValue("Contact", "jad.g.hajjar@hotmail.com");
					key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
					key.SetValue("UninstallString", exe + " /uninstallprompt");
				}
				finally
				{
					if (key != null)
					{
						key.Close();
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(
					"An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.",
					ex);
			}
		}
	}
}
