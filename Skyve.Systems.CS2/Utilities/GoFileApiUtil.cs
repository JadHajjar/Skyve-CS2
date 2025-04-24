using Extensions;

using Newtonsoft.Json;

using Skyve.Domain;

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
public class GoFileApiUtil
{
	private readonly ApiUtil _apiUtil;
	private readonly SkyveApiUtil _skyveApiUtil;

	public GoFileApiUtil(ApiUtil apiUtil, SkyveApiUtil skyveApiUtil)
	{
		_apiUtil = apiUtil;
		_skyveApiUtil = skyveApiUtil;
	}

	public async Task<string> UploadFile(string filePath)
	{
		var uploadInfo = await GetUploadData(filePath);

		if (CrossIO.FileExists(filePath + ".cid"))
		{
			await UploadFile(filePath + ".cid", uploadInfo);
		}

		return await UploadFile(filePath, uploadInfo);
	}

	private async Task<GoFileUploadInfo> GetUploadData(string filePath)
	{
		return await _skyveApiUtil.GetGoFileUploadInfo(Path.GetFileNameWithoutExtension(filePath) + "-" + Guid.NewGuid())
			?? throw new Exception("Could not get the required information to upload your file. Please try again later.");
	}

	private async Task<string> UploadFile(string filePath, GoFileUploadInfo uploadInfo)
	{
		using var httpClient = new HttpClient();
		using var form = new MultipartFormDataContent();

		var fileBytes = File.ReadAllBytes(filePath);
		var fileContent = new ByteArrayContent(fileBytes);
		fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

		form.Add(fileContent, "file", Path.GetFileName(filePath));

		form.Add(new StringContent(uploadInfo.FolderId), "folderId");

		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", uploadInfo.Token);

		var response = await httpClient.PostAsync($"https://{uploadInfo.ServerId}.gofile.io/contents/uploadfile", form);
		var responseContent = JsonConvert.DeserializeObject<CreateFileResult>(await response.Content.ReadAsStringAsync());

		return responseContent!.data!.downloadPage!;
	}

	private class CreateFileResult
	{
		public Data? data { get; set; }

		public class Data
		{
			public string? downloadPage { get; set; }
		}
	}
}
