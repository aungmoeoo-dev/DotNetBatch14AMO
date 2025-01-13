using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14AMO.Shared;

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
           ([BlogId],[BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogId,@BlogTitle
           ,@BlogAuthor
           ,@BlogContent);";

		string guid = Guid.NewGuid().ToString();
		requestModel.BlogId = guid;
		var result = connection.Execute(query, requestModel);

		string message = result > 0 ? "Saving successful." : "Saving failed.";

		BlogResponseModel model = new()
		{
			IsSuccessful = result > 0,
			Message = message,
			Data = result > 0 ? requestModel : null
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

		string conditionStr = string.Empty;

		if (!string.IsNullOrEmpty(requestModel.BlogTitle))
		{
			conditionStr += " [BlogTitle] = @BlogTitle, ";
			blog.BlogTitle = requestModel.BlogTitle;
		}
		if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
		{
			conditionStr += " [BlogAuthor] = @BlogAuthor, ";
			blog.BlogAuthor = requestModel.BlogAuthor;
		}
		if (!string.IsNullOrEmpty(requestModel.BlogContent))
		{
			conditionStr += " [BlogContent] = @BlogContent, ";
			blog.BlogContent = requestModel.BlogContent;
		}
		if (conditionStr.Length == 0)
		{
			throw new Exception("Invalid Parameters.");
		}

		conditionStr = conditionStr.Substring(0, conditionStr.Length - 2);

		string connectionString = _sqlConnectionStringBuilder.ConnectionString;
		using IDbConnection connection = new SqlConnection(connectionString);

		string query = $@"UPDATE [dbo].[TBL_Blog]
   SET {conditionStr} WHERE BlogId = @BlogId";

		var result = connection.Execute(query, blog);

		string message = result > 0 ? "Updating successful." : "Updating failed.";

		BlogResponseModel model = new()
		{
			IsSuccessful = result > 0,
			Message = message,
			Data = result > 0 ? requestModel : null
		};

		return model;
	}

	public BlogResponseModel UpsertBlog(BlogModel requestModel)
	{
		var responseModel = new BlogResponseModel();

		// check if the request has all the data
		if (requestModel.BlogTitle is null
			|| requestModel.BlogAuthor is null
			|| requestModel.BlogContent is null)
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
		responseModel.Data = result > 0 ? requestModel : null;
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
