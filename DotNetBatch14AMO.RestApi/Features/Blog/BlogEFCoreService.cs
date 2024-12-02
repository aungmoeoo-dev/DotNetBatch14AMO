
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14AMO.RestApi.Features.Blog;

public class BlogEFCoreService : IBlogService
{
	private AppDbContext _db;

	public BlogEFCoreService()
	{
		_db = new AppDbContext();
	}

	public BlogResponseModel CreateBlog(BlogModel requestModel)
	{
		requestModel.BlogId = Guid.NewGuid().ToString();
		_db.Blogs.Add(requestModel);
		int result = _db.SaveChanges();

		string message = result > 0 ? "Saving successful." : "Saving failed.";

		BlogResponseModel responseModel = new()
		{
			IsSuccessful = result > 0,
			Message = message
		};

		return responseModel;
	}

	public BlogResponseModel DeleteBlog(string id)
	{

		var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == id);

		if (blog is null) return new BlogResponseModel()
		{
			IsSuccessful = false,
			Message = "No data found."
		};

		_db.Entry(blog).State = EntityState.Deleted;
		int result = _db.SaveChanges();

		string message = result > 0 ? "Deleting successful." : "Deleting failed.";

		BlogResponseModel responseModel = new()
		{
			IsSuccessful = result > 0,
			Message = message
		};

		return responseModel;
	}

	public BlogModel GetBlog(string id)
	{
		var blog = _db.Blogs.FirstOrDefault(x => x.BlogId == id);

		return blog!;
	}

	public List<BlogModel> GetBlogs()
	{
		var blogs = _db.Blogs.ToList();

		return blogs;
	}

	public BlogResponseModel UpdateBlog(BlogModel requestModel)
	{
		var blog = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == requestModel.BlogId);

		if (blog is null) return new BlogResponseModel()
		{
			IsSuccessful = false,
			Message = "No data found."
		};

		if (!string.IsNullOrEmpty(requestModel.BlogTitle))
		{
			blog.BlogId = requestModel.BlogId;
		}
		else if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
		{
			blog.BlogAuthor = requestModel.BlogAuthor;
		}
		else if (!string.IsNullOrEmpty(requestModel.BlogContent))
		{
			blog.BlogContent = requestModel.BlogContent;
		}

		_db.Entry(blog).State = EntityState.Modified;
		int result = _db.SaveChanges();

		string message = result > 0 ? "Updating successful." : "Updating failed.";

		BlogResponseModel responseModel = new()
		{
			IsSuccessful = result > 0,
			Message = message
		};

		return responseModel;
	}

	public BlogResponseModel UpsertBlog(BlogModel requestModel)
	{
		BlogResponseModel responseModel = new();

		// // check if the request has all the data
		if (requestModel.BlogTitle is null || requestModel.BlogAuthor is null || requestModel.BlogContent is null)
		{
			responseModel.IsSuccessful = false;
			responseModel.Message = "Updating failed.";

			return responseModel;
		}

		var blog = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == requestModel.BlogId);

		if (blog is null)
		{
			return CreateBlog(requestModel);
		}

		blog.BlogTitle = requestModel.BlogTitle;
		blog.BlogAuthor = requestModel.BlogAuthor;
		blog.BlogContent = requestModel.BlogContent;

		_db.Entry(blog).State = EntityState.Modified;
		int result = _db.SaveChanges();

		string message = result > 0 ? "Updating successful." : "Updating failed.";

		responseModel.IsSuccessful = result > 0;
		responseModel.Message = message;
		return responseModel;
	}
}
