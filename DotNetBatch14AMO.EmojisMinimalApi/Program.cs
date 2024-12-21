using DotNetBatch14AMO.EmojisMinimalApi.Features.Emoji;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//EmojiService emojiService = new();
//await emojiService.InsertEmojis();

EmojiService emojiService = new EmojiService();

app.MapGet("/api/Emoji", async () =>
{
	EmojiListResponseModel responseModel = new();
	try
	{
		responseModel = await emojiService.GetEmojis();
		return Results.Ok(responseModel);
	}
	catch (Exception ex)
	{
		responseModel.IsSuccess = false;
		responseModel.Message = ex.Message;
		return Results.BadRequest(responseModel);
	}

})
.WithName("Get")
.WithOpenApi();

app.MapGet("/api/Emoji/{name}", async (string name) =>
{
	EmojiResponseModel responseModel = new();
	try
	{
		responseModel = await emojiService.GetEmojiById(name);

		if (!responseModel.IsSuccess) return Results.BadRequest(responseModel);

		return Results.Ok(responseModel);
	}
	catch (Exception ex)
	{
		responseModel.IsSuccess = false;
		responseModel.Message = ex.Message;
		return Results.BadRequest(responseModel);
	}

})
.WithName("GetById")
.WithOpenApi();

app.MapGet("/api/Emoji/FilterByName/{name}", async (string name) =>
{
	EmojiListResponseModel responseModel = new();
	try
	{
		responseModel = await emojiService.FilterByName(name);
		return Results.Ok(responseModel);
	}
	catch (Exception ex)
	{
		responseModel.IsSuccess = false;
		responseModel.Message = ex.Message;
		return Results.BadRequest(responseModel);
	}

})
.WithName("FilterByName")
.WithOpenApi();

app.Run();