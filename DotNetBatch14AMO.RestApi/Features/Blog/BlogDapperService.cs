using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14AMO.RestApi.Features.Blog;

public class BlogDapperService : IBlogService
{
	private SqlConnectionStringBuilder _sqlConnectionStringBuilder;

	public BlogDapperService()
	{
		_sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
		{
			DataSource = ".",
			InitialCatalog = "TestDB",
			UserID = "sa",
			Password = "Aa145156167!",
			TrustServerCertificate = true
		};
	}

	public List<BlogModel> GetBlogs()
	{
		string connectionString = _sqlConnectionStringBuilder.ConnectionString;
		using IDbConnection connection = new SqlConnection(connectionString);

		string query = "select * from TBL_Blog with (nolock)";
		var blogs = connection.Query<BlogModel>(query).ToList();

		return blogs;
	}

	public BlogModel GetBlog(string id)
	{
		string connectionString = _sqlConnectionStringBuilder.ConnectionString;
		using IDbConnection connection = new SqlConnection(connectionString);

		string query = "select * from TBL_Blog with (nolock) where BlogId = @BlogId";
		var blog = connection.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id });

		return blog!;
	}

	public BlogResponseModel CreateBlog(BlogModel requestModel)
	{
		string connectionString = _sqlConnectionStringBuilder.ConnectionString;
		using IDbConnection connection = new SqlConnection(connectionString);

		string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent);";
		var result = connection.Execute(query, requestModel);

		string message = result > 0 ? "Saving successful." : "Saving failed.";

		BlogResponseModel model = new()
		{
			IsSuccessful = result > 0,
			Message = message
		};

		return model;
	}

	public BlogResponseModel UpdateBlog(BlogModel requestModel)
	{
		var blog = GetBlog(requestModel.BlogId!);

		if (blog is null) return new()
		{
			IsSuccessful = false,
			Message = "No data found."
		};

		if (String.IsNullOrEmpty(requestModel.BlogTitle))
		{
			requestModel.BlogTitle = blog.BlogTitle;
		}
		else if (String.IsNullOrEmpty(requestModel.BlogAuthor))
		{
			requestModel.BlogAuthor = blog.BlogAuthor;
		}
		else if (String.IsNullOrEmpty(requestModel.BlogContent))
		{
			requestModel.BlogContent = blog.BlogContent;
		}

		string connectionString = _sqlConnectionStringBuilder.ConnectionString;
		using IDbConnection connection = new SqlConnection(connectionString);

		string query = $@"UPDATE [dbo].[TBL_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
		var result = connection.Execute(query, requestModel);

		string message = result > 0 ? "Updating successful." : "Updating failed.";

		BlogResponseModel model = new()
		{
			IsSuccessful = result > 0,
			Message = message
		};

		return model;
	}

	public BlogResponseModel UpsertBlog(BlogModel requestModel)
	{
		var responseModel = new BlogResponseModel();

		// check if the request has all the data
		if (requestModel.BlogTitle is null || requestModel.BlogAuthor is null || requestModel.BlogContent is null)
		{
			responseModel.IsSuccessful = false;
			responseModel.Message = "Updating failed.";

			return responseModel;
		}

		var blog = GetBlog(requestModel.BlogId!);

		if (blog is null)
		{
			return CreateBlog(requestModel);
		}

		string connectionString = _sqlConnectionStringBuilder.ConnectionString;
		using IDbConnection connection = new SqlConnection(connectionString);

		string query = $@"UPDATE [dbo].[TBL_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
		var result = connection.Execute(query, requestModel);

		string message = result > 0 ? "Updating successful." : "Updating failed.";

		responseModel.IsSuccessful = result > 0;
		responseModel.Message = message;

		return responseModel;
	}

	public BlogResponseModel DeleteBlog(string id)
	{
		string connectionString = _sqlConnectionStringBuilder.ConnectionString;
		using IDbConnection connection = new SqlConnection(connectionString);

		string query = "delete from TBL_Blog where BlogId = @BlogId";
		var result = connection.Execute(query, new BlogModel { BlogId = id });

		string message = result > 0 ? "Deleting successful." : "Deleting failed.";

		return new BlogResponseModel()
		{
			IsSuccessful = result > 0,
			Message = message
		};
	}
}
