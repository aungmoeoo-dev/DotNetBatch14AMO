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
		blogService = new BlogEFCoreService();
	}

	[HttpGet]
	public IActionResult GetBlogs()
	{
		try
		{
			var blogs = blogService.GetBlogs();

			BlogListResponseModel model = new()
			{
				IsSuccessful = true,
				Message = "Success",
				Data = blogs
			};

			return Ok(model);
		}
		catch (Exception ex)
		{
			BlogListResponseModel model = new()
			{
				IsSuccessful = false,
				Message = ex.ToString(),
				Data = null
			};

			return StatusCode(500, model);
		}
	}

	[HttpGet("{id}")]
	public IActionResult GetBlog(string id)
	{
		try
		{
			var blog = blogService.GetBlog(id);

			if (blog == null) return NotFound(new BlogResponseModel()
			{
				Message = "No data found.",
			});

			BlogResponseModel model = new()
			{
				IsSuccessful = true,
				Message = "Success",
				Data = blog
			};

			return Ok(model);
		}
		catch (Exception ex)
		{
			BlogResponseModel model = new()
			{
				IsSuccessful = false,
				Message = ex.ToString(),
				Data = null
			};

			return StatusCode(500, model);
		}
	}

	[HttpPost]
	public IActionResult CreateBlog([FromBody] BlogModel requestModel)
	{
		try
		{
			var model = blogService.CreateBlog(requestModel);

			if (!model.IsSuccessful) return BadRequest(model);

			return Ok(model);

		}
		catch (Exception ex)
		{
			BlogResponseModel model = new()
			{
				IsSuccessful = false,
				Message = ex.ToString(),
				Data = null
			};

			return StatusCode(500, model);
		}
	}

	[HttpPut("{id}")]
	public IActionResult UpsertBlog(string id, [FromBody] BlogModel requestModel)
	{
		try
		{
			requestModel.BlogId = id;

			var model = blogService.UpsertBlog(requestModel);

			if (!model.IsSuccessful) return BadRequest(model);

			return Ok(model);
		}
		catch (Exception ex)
		{
			BlogResponseModel blogResponseModel = new()
			{
				IsSuccessful = false,
				Message = ex.ToString(),
				Data = null
			};

			return StatusCode(500, blogResponseModel);
		}
	}

	[HttpPatch("{id}")]
	public IActionResult UpdateBlog(string id, [FromBody] BlogModel requestModel)
	{
		try
		{
			requestModel.BlogId = id;

			var model = blogService.UpdateBlog(requestModel);

			if (!model.IsSuccessful) return BadRequest(model);

			return Ok(model);
		}
		catch (Exception ex)
		{
			BlogResponseModel blogResponseModel = new()
			{
				IsSuccessful = false,
				Message = ex.ToString(),
				Data = null
			};
			return StatusCode(500, blogResponseModel);
		}
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteBlog(string id)
	{
		try
		{
			var model = blogService.DeleteBlog(id);

			if (!model.IsSuccessful) return NotFound(model);

			return Ok(model);
		}
		catch (Exception ex)
		{
			BlogResponseModel blogResponseModel = new()
			{
				IsSuccessful = false,
				Message = ex.ToString(),
				Data = null
			};
			return StatusCode(500, blogResponseModel);
		}
	}
}
