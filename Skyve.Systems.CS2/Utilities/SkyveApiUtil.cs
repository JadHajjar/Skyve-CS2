﻿using Extensions;

using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Domain;

using SkyveApi.Domain.CS2;
using SkyveApi.Domain.Generic;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
public class SkyveApiUtil : ISkyveApiUtil
{
	private readonly ApiUtil _apiUtil;
	private readonly IUserService _userService;

	public SkyveApiUtil(ApiUtil apiUtil, IUserService userService)
	{
		_apiUtil = apiUtil;
		_userService = userService;
	}

	public async Task<T?> Get<T>(string url, params (string, object)[] queryParams)
	{
		return await _apiUtil.Get<T>(KEYS.API_URL + url
			, new[] { ("API_KEY", KEYS.API_KEY), ("USER_ID", Encryption.Encrypt(_userService.User.Id?.ToString() ?? string.Empty, KEYS.SALT)) }
			, queryParams);
	}

	public async Task<T?> Delete<T>(string url, params (string, object)[] queryParams)
	{
		return await _apiUtil.Delete<T>(KEYS.API_URL + url
			, new[] { ("API_KEY", KEYS.API_KEY), ("USER_ID", Encryption.Encrypt(_userService.User.Id?.ToString() ?? string.Empty, KEYS.SALT)) }
			, queryParams);
	}

	public async Task<T?> Post<TBody, T>(string url, TBody body, params (string, object)[] queryParams)
	{
		return await _apiUtil.Post<TBody, T>(KEYS.API_URL + url
			, body
			, new[] { ("API_KEY", KEYS.API_KEY), ("USER_ID", Encryption.Encrypt(_userService.User.Id?.ToString() ?? string.Empty, KEYS.SALT)) }
			, queryParams);
	}

	public async Task<bool> IsCommunityManager()
	{
		return await Get<bool>("/IsCommunityManager");
	}

	public async Task<CompatibilityData?> Catalogue()
	{
		return await Get<CompatibilityData>("/Catalogue");
	}

	public async Task<CompatibilityData?> Catalogue(object steamId)
	{
		return await Get<CompatibilityData>("/Package", ("steamId", steamId));
	}

	public async Task<ApiResponse> SaveEntry(PostPackage package)
	{
		return await Post<PostPackage, ApiResponse>("/SaveEntry", package);
	}

	public async Task<Dictionary<string, string>?> Translations()
	{
		return await Get<Dictionary<string, string>>("/Translations");
	}

	public async Task<ApiResponse> SendReviewRequest(ReviewRequest request)
	{
		return await Post<ReviewRequest, ApiResponse>("/RequestReview", request);
	}

	public async Task<ApiResponse> ProcessReviewRequest(ReviewRequest request)
	{
		return await Post<ReviewRequest, ApiResponse>("/ProcessReviewRequest", request);
	}

	public async Task<ReviewRequest[]?> GetReviewRequests()
	{
		return await Get<ReviewRequest[]>("/GetReviewRequests");
	}

	public async Task<ReviewRequest?> GetReviewRequest(ulong userId, ulong packageId)
	{
		return await Get<ReviewRequest>("/GetReviewRequest", (nameof(userId), userId), (nameof(packageId), packageId));
	}

	public async Task<IOnlinePlayset[]?> GetUserPlaysets(IUser userId)
	{
		return await Task.FromResult(new IOnlinePlayset[0]);
		//return (await Get<UserProfile[]>("/GetUserProfiles", (nameof(userId), userId)))?.ToArray(x => new OnlinePlayset(x));
	}

	//public async Task<IOnlinePlayset?> GetUserProfileContents(int profileId)
	//{
	//	var profile =  await Get<UserProfile>("/GetUserProfileContents", (nameof(profileId), profileId));

	//	return profile is null ? null : new OnlinePlayset(profile);
	//}

	//public async Task<IOnlinePlayset?> GetUserProfileByLink(string link)
	//{
	//	var profile = await Get<UserProfile>("/GetUserProfileByLink", (nameof(link), link));

	//	return profile is null ? null : new OnlinePlayset(profile);
	//}

	public async Task<ApiResponse> DeleteUserProfile(int profileId)
	{
		return await Delete<ApiResponse>("/DeleteUserProfile", (nameof(profileId), profileId));
	}

	public async Task<ApiResponse> SaveUserProfile(UserProfile profile)
	{
		return await Post<UserProfile, ApiResponse>("/SaveUserProfile", profile);
	}

	public async Task<IOnlinePlayset[]?> GetPublicPlaysets()
	{
		return await Task.FromResult(new IOnlinePlayset[0]);
		//return (await Get<UserProfile[]>("/GetPublicProfiles"))?.ToArray(x => new OnlinePlayset(x));
	}

	public async Task<ApiResponse> SetProfileVisibility(int profileId, bool @public)
	{
		return await Post<bool, ApiResponse>("/SetProfileVisibility", @public, (nameof(profileId), profileId));
	}

	public async Task<ApiResponse> GetUserGuid()
	{
		return await Get<ApiResponse>("/GetUserGuid");
	}
}