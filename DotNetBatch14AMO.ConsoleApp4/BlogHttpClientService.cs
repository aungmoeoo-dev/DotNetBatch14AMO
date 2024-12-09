using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetBatch14AMO.ConsoleApp4;

public class BlogHttpClientService
{
	private readonly string endpoint = "https://localhost:7206/api/blog";
	private HttpClient _httpClient;

	public BlogHttpClientService()
	{
		this._httpClient = new HttpClient();
	}

	public async Task<BlogListResponseModel> GetBlogs()
	{
		HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
		string content = await response.Content.ReadAsStringAsync();
		Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogListResponseModel>(content)!;
	}

	public async Task<BlogResponseModel> GetBlog(string id)
	{
		HttpResponseMessage response = await _httpClient.GetAsync($"{endpoint}/{id}");
		string content = await response.Content.ReadAsStringAsync();
		Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogResponseModel>(content)!;
	}

	public async Task<BlogResponseModel> CreateBlog(BlogModel requestModel)
	{
		string jsonStr = JsonConvert.SerializeObject(requestModel);
		var stringContent = new StringContent(jsonStr, Encoding.UTF8, Application.Json);

		HttpResponseMessage response = await _httpClient.PostAsync(endpoint, stringContent);
		string content = await response.Content.ReadAsStringAsync();
		Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogResponseModel>(content)!;
	}

	public async Task<BlogResponseModel> UpdateBlog(BlogModel requestModel)
	{
		string jsonStr = JsonConvert.SerializeObject(requestModel);
		var stringContent = new StringContent(jsonStr, Encoding.UTF8, Application.Json);

		var response = await _httpClient.PatchAsync($"{endpoint}/{requestModel.BlogId}", stringContent);
		string content = await response.Content.ReadAsStringAsync();
		Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogResponseModel>(content)!;
	}

	public async Task<BlogResponseModel> DeleteBlog(string id)
	{
		HttpResponseMessage response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
		string content = await response.Content.ReadAsStringAsync();
		Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogResponseModel>(content)!;
	}
}
