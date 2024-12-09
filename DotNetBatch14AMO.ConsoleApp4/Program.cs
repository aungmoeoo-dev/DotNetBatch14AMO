// See https://aka.ms/new-console-template for more information

using DotNetBatch14AMO.ConsoleApp4;

string endpoint = "https://localhost:7206/api/blog";
HttpClient client = new();
HttpResponseMessage response = await client.GetAsync(endpoint);

if (response.IsSuccessStatusCode)
{
	string content = await response.Content.ReadAsStringAsync();
	Console.WriteLine(content);
}

//BlogModel model = new()
//{
//	BlogTitle = "HttpClient Test Title",
//	BlogAuthor = "",
//	BlogContent = ""
//};
//BlogHttpClientService httpClientService = new();
//BlogResponseModel responseModel = await httpClientService.CreateBlog(model);

//Console.WriteLine(responseModel.Data.BlogId);
//Console.WriteLine(responseModel.Data.BlogTitle);
//Console.WriteLine(responseModel.Data.BlogAuthor);
//Console.WriteLine(responseModel.Data.BlogContent);