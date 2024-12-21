using DotNetBatch14AMO.EmojisMinimalApi.Utilities;
using Newtonsoft.Json;
using System.Data;

namespace DotNetBatch14AMO.EmojisMinimalApi.Features.Emoji;

public class EmojiService
{
	private readonly HttpClient _httpClient;
	private readonly string _endpoint;
	private List<EmojiModel> Emojis { get; set; }

	public EmojiService()
	{
		_endpoint = "https://gist.githubusercontent.com/oliveratgithub/0bf11a9aff0d6da7b46f1490f86a71eb/raw/d8e4b78cfe66862cf3809443c1dba017f37b61db/emojis.json";
		_httpClient = new HttpClient();
	}

	private async Task LoadEmojis()
	{
		EmojiRequestModel requestModel = await _httpClient.GetFromJsonAsync<EmojiRequestModel>(_endpoint);

		Emojis = requestModel.Emojis;
	}

	public async Task<EmojiListResponseModel> GetEmojis()
	{
		if (Emojis is null)
		{
			await LoadEmojis();
		}

		EmojiListResponseModel responseModel = new()
		{
			IsSuccess = true,
			Message = "Success",
			Data = Emojis!
		};
		return responseModel;
	}

	public async Task<EmojiResponseModel> GetEmojiById(string name)
	{
		EmojiResponseModel responseModel = new();

		if (Emojis is null)
		{
			await LoadEmojis();
		}

		var emojiModel = Emojis!.Where(x => x.Name == name).FirstOrDefault();

		if (emojiModel is null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "No data found.";
			return responseModel;
		}

		responseModel.IsSuccess = true;
		responseModel.Message = "Success";
		responseModel.Data = emojiModel;
		return responseModel;
	}

	public async Task<EmojiListResponseModel> FilterByName(string name)
	{
		EmojiListResponseModel responseModel = new();

		if (Emojis is null)
		{
			await LoadEmojis();
		}

		var emojis = Emojis!
			.Where(x => x.Name.ToLower().Contains(name.ToLower()))
			.ToList();

		responseModel.IsSuccess = true;
		responseModel.Message = "Success";
		responseModel.Data = emojis;

		return responseModel;
	}
}
