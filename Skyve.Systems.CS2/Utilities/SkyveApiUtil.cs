using Extensions;

using Skyve.Compatibility.Domain;
using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Domain.Api;
using Skyve.Systems.CS2.Domain.Api.DTO;
using Skyve.Systems.CS2.Domain.DTO;

using SkyveApi.Domain.CS2;
using SkyveApi.Domain.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
public class SkyveApiUtil
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
			, [("API_KEY", KEYS.API_KEY), ("USER_ID", Encryption.Encrypt(_userService.User.Id?.ToString() ?? string.Empty, KEYS.SALT))]
			, queryParams);
	}

	public async Task<T?> Delete<T>(string url, params (string, object)[] queryParams)
	{
		return await _apiUtil.Delete<T>(KEYS.API_URL + url
			, [("API_KEY", KEYS.API_KEY), ("USER_ID", Encryption.Encrypt(_userService.User.Id?.ToString() ?? string.Empty, KEYS.SALT))]
			, queryParams);
	}

	public async Task<T?> Post<TBody, T>(string url, TBody body, params (string, object)[] queryParams)
	{
		return await _apiUtil.Post<TBody, T>(KEYS.API_URL + url
			, body
			, [("API_KEY", KEYS.API_KEY), ("USER_ID", Encryption.Encrypt(_userService.User.Id?.ToString() ?? string.Empty, KEYS.SALT))]
			, queryParams);
	}

	private TObj? ConvertDto<TData, TObj, TDto>(TData? data) where TDto : IDTO<TData, TObj>, new()
	{
		return data is null ? default : new TDto().Convert(data);
	}

	private TObj[] ConvertDto<TData, TObj, TDto>(IEnumerable<TData>? data) where TDto : IDTO<TData, TObj>, new()
	{
		var dto = new TDto();

		return data?.ToArray(dto.Convert) ?? [];
	}

	public async Task<Dictionary<string, string>?> Translations()
	{
		return await Get<Dictionary<string, string>>("/Translations");
	}

	public async Task<IKnownUser[]> GetUsers()
	{
		return ConvertDto<UserData, User, UserDto>(await Get<UserData[]>("/Users"));
	}

	public async Task<PackageData[]> GetPackageData()
	{
		return ConvertDto<CompatibilityPackageData, PackageData, PackageDataDto>(await Get<CompatibilityPackageData[]>("/CompatibilityData"));
	}

	public async Task<PackageData?> GetPackageData(ulong packageId)
	{
		return ConvertDto<CompatibilityPackageData, PackageData, PackageDataDto>(await Get<CompatibilityPackageData>("/CompatibilityData/" + packageId));
	}

	public async Task<Blacklist> GetBlacklist()
	{
		return await Get<Blacklist>("/Blacklist") ?? new();
	}

	public async Task<ApiResponse> UpdatePackageData(CompatibilityPostPackage postPackage)
	{
		return await Post<PostPackage, ApiResponse>("/UpdatePackageData", ConvertDto<CompatibilityPostPackage, PostPackage, PostPackageDto>(postPackage)!);
	}

	public async Task<ApiResponse> SendReviewRequest(ReviewRequest request)
	{
		return await Post<ReviewRequestData, ApiResponse>("/RequestReview", ConvertDto<ReviewRequest, ReviewRequestData, ReviewRequestDto>(request)!);
	}

	public async Task<ApiResponse> ProcessReviewRequest(ReviewRequest request)
	{
		return await Post<ReviewRequestData, ApiResponse>("/ProcessReviewRequest", ConvertDto<ReviewRequest, ReviewRequestData, ReviewRequestDto>(request)!);
	}

	public async Task<ReviewRequest[]?> GetReviewRequests()
	{
		return ConvertDto<ReviewRequestData, ReviewRequest, ReviewRequestDto>(await Get<ReviewRequestData[]>("/GetReviewRequests"));
	}

	public async Task<ReviewRequest?> GetReviewRequest(string userId, ulong packageId)
	{
		return ConvertDto<ReviewRequestData, ReviewRequest, ReviewRequestDto>(await Get<ReviewRequestData>("/GetReviewRequest", (nameof(userId), userId), (nameof(packageId), packageId)));
	}

	public async Task<INotificationInfo[]?> GetAnnouncements()
	{
		return ConvertDto<AnnouncementData, AnnouncementNotification, AnnouncementDto>(await Get<AnnouncementData[]>("/Announcements"));
	}

	public async Task<PackageEdit[]?> GetPackageEdits(ulong packageId)
	{
		return ConvertDto<PackageEditData, PackageEdit, PackageEditDto>(await Get<PackageEditData[]>("/GetPackageEdits", (nameof(packageId), packageId)));
	}

	public async Task<ReviewReply[]?> GetReviewMessages()
	{
		return ConvertDto<ReviewReplyData, ReviewReply, ReviewReplyDto>(await Get<ReviewReplyData[]>("/GetReviewMessages"));
	}

	public async Task<ReviewReply?> GetReviewStatus(ulong packageId)
	{
		return ConvertDto<ReviewReplyData, ReviewReply, ReviewReplyDto>(await Get<ReviewReplyData>("/GetReviewStatus", (nameof(packageId), packageId)));
	}

	public async Task<ApiResponse> SendReviewMessage(ReviewReply reply)
	{
		return await Post<ReviewReplyData, ApiResponse>("/SendReviewMessage", ConvertDto<ReviewReply, ReviewReplyData, ReviewReplyDataDto>(reply)!);
	}

	public async Task<ApiResponse> DeleteReviewMessage(ulong packageId)
	{
		return await Delete<ApiResponse>("/DeleteReviewMessage", (nameof(packageId), packageId));
	}

	public async Task<GoFileInfo?> GetGoFileInfo()
	{
		return ConvertDto<GoFileInfoData, GoFileInfo, GoFileInfoDto>(await Get<GoFileInfoData>("/GetGoFileInfo"));
	}
}
