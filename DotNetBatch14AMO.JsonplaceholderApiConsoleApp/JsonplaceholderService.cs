using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14AMO.JsonplaceholderApiConsoleApp;

public class JsonplaceholderService
{
	private readonly IJsonplaceholderApi _jsonplaceholderApi;

	public JsonplaceholderService()
	{
		_jsonplaceholderApi = RestService.For<IJsonplaceholderApi>("https://jsonplaceholder.typicode.com");
	}

	public async Task<List<PostModel>> GetPosts()
	{
		return await _jsonplaceholderApi.GetPosts();
	}

	public async Task<PostModel> GetPost(int id)
	{
		return await _jsonplaceholderApi.GetPost(id);
	}

	public async Task<PostModel> CreatePost(PostModel requestModel)
	{
		return await _jsonplaceholderApi.CreatePost(requestModel);
	}

	public async Task<PostModel> UpdatePost(int id, PostModel requestModel)
	{
		return await _jsonplaceholderApi.UpdatePost(id, requestModel);
	}

	public async Task<PostModel> DeletePost(int id)
	{
		return await _jsonplaceholderApi.DeletePost(id);
	}
}
