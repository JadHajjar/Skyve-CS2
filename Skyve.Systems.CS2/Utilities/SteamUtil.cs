using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Skyve.Systems.CS2.Utilities;

public static class SteamUtil
{
	private const string LibraryName = "steam_api64";

	public static bool TryGetSteamUserId(out ulong userId)
	{
		var currentUser = GetSteamUserInstance();
		if (currentUser != System.IntPtr.Zero)
		{
			userId = ISteamUser_GetSteamID(currentUser);
		}
		else
		{
			userId = 0;
		}


		return userId != 0;
	}

	public static bool TryGetAppInstallDir(uint appId, out string pchFolder, int bufferSize = 260)
	{
		var appsPtr = GetSteamAppsInstance();

		var sb = new StringBuilder(bufferSize); // MAX_PATH size

		var length = ISteamApps_GetAppInstallDir(appsPtr, appId, sb, (uint)sb.Capacity);

		if (length > 0)
		{
			pchFolder = sb.ToString();
			return true;
		}
		else
		{
			pchFolder = string.Empty;
			return false;
		}
	}

	public static bool IsDlcOwned(uint appId)
	{
		var appsPtr = GetSteamAppsInstance();

		return ISteamApps_BIsSubscribedApp(appsPtr, appId);
	}

	[DllImport(LibraryName, EntryPoint = "SteamAPI_IsSteamRunning", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	public static extern bool IsSteamRunning();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_Init", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	public static extern bool InitSteamAPI();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl)]
	public static extern void ShutdownSteamAPI();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamUser_v023", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr GetSteamUserInstance();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_ISteamUser_GetSteamID", CallingConvention = CallingConvention.Cdecl)]
	private static extern ulong ISteamUser_GetSteamID(IntPtr instancePtr);

	[DllImport(LibraryName, EntryPoint = "SteamAPI_SteamApps_v008", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr GetSteamAppsInstance();

	[DllImport(LibraryName, EntryPoint = "SteamAPI_ISteamApps_GetAppInstallDir", CallingConvention = CallingConvention.Cdecl)]
	private static extern uint ISteamApps_GetAppInstallDir(IntPtr instancePtr, uint appId, StringBuilder folder, uint folderBufferSize);

	[DllImport(LibraryName, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedApp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	public static extern bool ISteamApps_BIsSubscribedApp(IntPtr instancePtr, uint appId);
}
