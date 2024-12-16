using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14AMO.JsonplaceholderApiConsoleApp;

public interface IJsonplaceholderApi
{
	[Get("/posts")]
	public Task<List<PostModel>> GetPosts();
	
	[Get("/posts/{id}")]
	public Task<PostModel> GetPost(int id);

	[Post("/posts")]
	public Task<PostModel> CreatePost(PostModel requestModel);

	[Patch("/posts/{id}")]
	public Task<PostModel> UpdatePost(int id, PostModel requestModel);

	[Delete("/posts/{id}")]
	public Task<PostModel> DeletePost(int id);
}
