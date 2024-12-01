using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14AMO.RestApi.Features.Blog;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
	private IBlogService blogService;

	public BlogController()
	{
		blogService = new BlogDapperService();
	}

	[HttpGet]
	public IActionResult GetBlogs()
	{
		var blogs = blogService.GetBlogs();

		return Ok(blogs);
	}

	[HttpGet("{id}")]
	public IActionResult GetBlog(string id)
	{
		var blog = blogService.GetBlog(id);

		if (blog == null) return NotFound("No data found.");

		return Ok(blog);
	}

	[HttpPost]
	public IActionResult CreateBlog([FromBody] BlogModel requestModel)
	{
		var model = blogService.CreateBlog(requestModel);

		if (!model.IsSuccessful) return BadRequest(model);

		return Ok(model);
	}

	[HttpPut("{id}")]
	public IActionResult UpsertBlog(string id, [FromBody] BlogModel requestModel)
	{
		requestModel.BlogId = id;

		var model = blogService.UpsertBlog(requestModel);

		if (!model.IsSuccessful) return BadRequest(model);

		return Ok(model);
	}

	[HttpPatch("{id}")]
	public IActionResult UpdateBlog(string id, [FromBody] BlogModel requestModel)
	{
		requestModel.BlogId = id;

		var model = blogService.UpdateBlog(requestModel);

		if(!model.IsSuccessful) return BadRequest(model);

		return Ok(model);
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteBlog(string id)
	{
		var model = blogService.DeleteBlog(id);

		if (!model.IsSuccessful) return NotFound(model);

		return Ok(model);
	}
}
