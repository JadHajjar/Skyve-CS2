using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Steam;
using Skyve.Domain.Systems;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;

public static class SteamUtil
{
	private const string LibraryName = "steam_api64";

	public static string GetSteamUserId()
	{
		var currentUser = SteamAPI_SteamUser_v023();
		if (currentUser == System.IntPtr.Zero)
		{
			return string.Empty;
		}

		var userId = ISteamUser_GetSteamID(currentUser);
		if (userId == 0)
		{
			return string.Empty;
		}

		return userId.ToString();
	}

	[DllImport(LibraryName, EntryPoint = "SteamAPI_IsSteamRunning", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	public static extern bool SteamAPI_IsSteamRunning();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_Init", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	public static extern bool SteamAPI_Init();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SteamAPI_Shutdown();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamUser_v023", CallingConvention = CallingConvention.Cdecl)]
	public static extern System.IntPtr SteamAPI_SteamUser_v023();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_ISteamUser_GetSteamID", CallingConvention = CallingConvention.Cdecl)]
	public static extern ulong ISteamUser_GetSteamID(System.IntPtr instancePtr);

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamApps_v012", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr SteamAPI_SteamApps1();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamApps_v013", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr SteamAPI_SteamApps2();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamApps_v014", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr SteamAPI_SteamApps3();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamApps_v015", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr SteamAPI_SteamApps4();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamApps_v016", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr SteamAPI_SteamApps5();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamApps_v017", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr SteamAPI_SteamApps6();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamApps_v018", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr SteamAPI_SteamApps7();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_ISteamApps_GetAppInstallDir",		CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern uint SteamAPI_ISteamApps_GetAppInstallDir(		IntPtr instancePtr, uint appId, StringBuilder folder, uint folderBufferSize);

	public static bool TryGetAppInstallDir(uint appId, out string pchFolder, uint bufferSize = 1024)
	{
		pchFolder = string.Empty;

		IntPtr apps1 = SteamAPI_SteamApps1(); // get ISteamApps* pointer
		IntPtr apps2 = SteamAPI_SteamApps2(); // get ISteamApps* pointer
		IntPtr apps3 = SteamAPI_SteamApps3(); // get ISteamApps* pointer
		IntPtr apps4 = SteamAPI_SteamApps4(); // get ISteamApps* pointer
		IntPtr apps5 = SteamAPI_SteamApps5(); // get ISteamApps* pointer
		IntPtr apps6 = SteamAPI_SteamApps6(); // get ISteamApps* pointer
		IntPtr apps7 = SteamAPI_SteamApps7(); // get ISteamApps* pointer
		if (apps1 == IntPtr.Zero)
			return false;

		var sb = new StringBuilder((int)bufferSize);
		uint len = SteamAPI_ISteamApps_GetAppInstallDir(apps, appId, sb, bufferSize);

		if (len > 0)
			pchFolder = sb.ToString();

		return len > 0;
	}
}
