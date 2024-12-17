using Dapper;
using DotNetBatch14AMO.EmojisMinimalApi.Utilities;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace DotNetBatch14AMO.EmojisMinimalApi.Features.Emoji;

public class EmojiService
{
	private readonly HttpClient _httpClient;
	private readonly string _endpoint;

	private readonly string _connectionString;
	private readonly SqlConnectionStringBuilder _sqlconnectionStringBuilder = new()
	{
		DataSource = ".",
		InitialCatalog = "EmojiDb",
		UserID = "sa",
		Password = "Aa145156167!",
		TrustServerCertificate = true
	};

	public EmojiService()
	{
		_connectionString = _sqlconnectionStringBuilder.ConnectionString;
		_endpoint = "https://gist.githubusercontent.com/oliveratgithub/0bf11a9aff0d6da7b46f1490f86a71eb/raw/d8e4b78cfe66862cf3809443c1dba017f37b61db/emojis.json";
		_httpClient = new HttpClient();
	}

	public async Task InsertEmojis()
	{
		HttpResponseMessage responseMessage = await _httpClient.GetAsync(_endpoint);
		string content = await responseMessage.Content.ReadAsStringAsync();

		EmojiRequestModel requestModel = JsonConvert.DeserializeObject<EmojiRequestModel>(content)!;

		List<EmojiModel> list = requestModel.Emojis;

		using IDbConnection connection = new SqlConnection(_connectionString);

		foreach (var emoji in list)
		{
			string query = @"INSERT INTO [dbo].[TBL_Emoji]
           ([Emoji]
           ,[EmojiName]
           ,[EmojiShortname]
           ,[EmojiUnicode]
           ,[EmojiHtml]
           ,[EmojiCategory]
           ,[EmojiOrder])
     VALUES
           (@Emoji
           ,@Name
           ,@Shortname
           ,@Unicode
           ,@Html
           ,@Category
           ,@Order)";

			connection.Execute(query, emoji);
		}
	}

	public async Task<EmojiListResponseModel> GetEmojis()
	{
		using IDbConnection connection = new SqlConnection(_connectionString);

		string query = "select * from TBL_Emoji";

		var emojiEnumerable = await connection.QueryAsync<EmojiModel>(query);
		var emojiList = emojiEnumerable.ToList();

		EmojiListResponseModel responseModel = new()
		{
			IsSuccess = true,
			Message = "Success",
			Data = emojiList
		};
		return responseModel;
	}

	public async Task<EmojiResponseModel> GetEmojiById(string id)
	{
		EmojiResponseModel responseModel = new();

		using IDbConnection connection = new SqlConnection(_connectionString);

		string query = "select * from TBL_Emoji where EmojiId = @EmojiId";

		var emojiModel = await connection.QueryFirstOrDefaultAsync<EmojiModel>(query, new { EmojiId = id });
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

		using IDbConnection connection = new SqlConnection(_connectionString);

		string query = "select * from TBL_Emoji where EmojiName like @EmojiName";

		var emojiEnumerable = await connection.QueryAsync<EmojiModel>(query, new { EmojiName = $"%{name}%" });
		var emojiList = emojiEnumerable.ToList();

		responseModel.IsSuccess = true;
		responseModel.Message = "Success";
		responseModel.Data = emojiList;

		return responseModel;
	}
}
