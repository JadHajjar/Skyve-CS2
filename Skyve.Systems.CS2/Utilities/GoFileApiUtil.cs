using Newtonsoft.Json;

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

	private string? Token;
	private string? RootFolder;

	public GoFileApiUtil(ApiUtil apiUtil, SkyveApiUtil skyveApiUtil)
	{
		_apiUtil = apiUtil;
		_skyveApiUtil = skyveApiUtil;
	}

	public async Task<string> UploadFile(string filePath)
	{
		await ValidateToken();

		var folderId = await CreateFolder(filePath);
		var server = await GetServer();

		using var httpClient = new HttpClient();
		using var form = new MultipartFormDataContent();

		var fileBytes = File.ReadAllBytes(filePath);
		var fileContent = new ByteArrayContent(fileBytes);
		fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
		
		form.Add(fileContent, "file", Path.GetFileName(filePath));
		form.Add(new StringContent(folderId), "folderId");

		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

		var response = await httpClient.PostAsync($"https://{server}.gofile.io/contents/uploadfile", form);
		var responseContent = JsonConvert.DeserializeObject<CreateFileResult>(await response.Content.ReadAsStringAsync());

		return responseContent!.data!.downloadPage!;
	}

	private async Task ValidateToken()
	{
		if (Token is not null)
		{
			return;
		}

		var info = await _skyveApiUtil.GetGoFileInfo();

		Token = info.Token;
		RootFolder = info.RootFolder;
	}

	private async Task<string> CreateFolder(string filePath)
	{
		var folder = new CreateFolderPayload
		{
			parentFolderId = RootFolder!,
			folderName = Path.GetFileNameWithoutExtension(filePath) + "-" + Guid.NewGuid()
		};

		var result = await _apiUtil.Post<CreateFolderPayload, CreateFolderPayload.Result>("https://api.gofile.io/contents/createFolder", folder, headers: [("Authorization", "Bearer " + Token)]);

		return result!.data!.folderId!;
	}

	private async Task<string> GetServer()
	{
		var servers = await _apiUtil.Get<ServerPayload>("https://api.gofile.io/servers");

		return servers!.data!.servers![0].name!;
	}

	private class ServerPayload
	{
		public Data? data { get; set; }

		public class Data
		{
			public Server[]? servers { get; set; }
		}

		public class Server
		{
			public string? name { get; set; }
		}
	}

	private class CreateFolderPayload
	{
		public string? parentFolderId { get; set; }
		public string? folderName { get; set; }


		public class Result
		{
			public Data? data { get; set; }

			public class Data
			{
				public string? folderId { get; set; }
			}
		}
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
