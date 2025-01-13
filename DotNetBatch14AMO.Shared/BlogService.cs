using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14AMO.Shared;

public class BlogService : IBlogService
{
	private SqlConnectionStringBuilder _SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
	{
		DataSource = ".",
		InitialCatalog = "TestDB",
		UserID = "sa",
		Password = "Aa145156167!",
		TrustServerCertificate = true
	};

	public List<BlogModel> GetBlogs()
	{
		string connectionString = _SqlConnectionStringBuilder.ConnectionString;
		SqlConnection connection = new SqlConnection(connectionString);

		connection.Open();

		SqlCommand cmd = new SqlCommand("select * from TBL_Blog", connection);
		SqlDataAdapter adapter = new SqlDataAdapter(cmd);

		DataTable dt = new();
		adapter.Fill(dt);

		connection.Close();

		var list = new List<BlogModel>();

		foreach (DataRow row in dt.Rows)
		{
			BlogModel blog = new()
			{
				BlogId = row["BlogId"].ToString(),
				BlogTitle = row["BlogTitle"].ToString(),
				BlogAuthor = row["BlogAuthor"].ToString(),
				BlogContent = row["BlogContent"].ToString()
			};

			list.Add(blog);
		}

		return list;
	}

	public BlogModel GetBlog(string id)
	{
		string connectionString = _SqlConnectionStringBuilder.ConnectionString;
		SqlConnection connection = new(connectionString);

		connection.Open();

		SqlCommand cmd = new("select * from TBL_Blog where BlogId = @BlogId", connection);
		cmd.Parameters.AddWithValue("@BlogId", id);

		SqlDataAdapter adapter = new SqlDataAdapter(cmd);

		DataTable dt = new();
		adapter.Fill(dt);

		connection.Close();

		if (dt.Rows.Count == 0) return null;

		DataRow row = dt.Rows[0];

		BlogModel blog = new()
		{
			BlogId = row["BlogId"].ToString(),
			BlogTitle = row["BlogTitle"].ToString(),
			BlogAuthor = row["BlogAuthor"].ToString(),
			BlogContent = row["BlogContent"].ToString()
		};

		return blog;
	}

	public BlogResponseModel CreateBlog(BlogModel requestModel)
	{
		string connectionString = _SqlConnectionStringBuilder.ConnectionString;
		SqlConnection connection = new(connectionString);

		connection.Open();

		string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogId],[BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogId,@BlogTitle
           ,@BlogAuthor
           ,@BlogContent);";

		SqlCommand cmd = new(query, connection);
		string guid = Guid.NewGuid().ToString();
		requestModel.BlogId = guid;

		cmd.Parameters.AddWithValue("@BlogId", requestModel.BlogId);
		cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
		cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
		cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
		int result = cmd.ExecuteNonQuery();

		connection.Close();

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

		string connectionString = _SqlConnectionStringBuilder.ConnectionString;
		SqlConnection connection = new(connectionString);

		connection.Open();

		string query = $@"UPDATE [dbo].[TBL_Blog]
   SET {conditionStr} WHERE BlogId = @BlogId";

		SqlCommand cmd = new(query, connection);
		cmd.Parameters.AddWithValue("@BlogId", requestModel.BlogId);

		if (!string.IsNullOrEmpty(requestModel.BlogTitle))
		{
			cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
		}
		if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
		{
			cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
		}
		if (!string.IsNullOrEmpty(requestModel.BlogContent))
		{
			cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
		}

		int result = cmd.ExecuteNonQuery();

		connection.Close();

		string message = result > 0 ? "Updating successful." : "Updating failed.";

		BlogResponseModel responseModel = new()
		{
			IsSuccessful = result > 0,
			Message = message,
			Data = result > 0 ? blog : null
		};

		return responseModel;
	}

	public BlogResponseModel UpsertBlog(BlogModel requestModel)
	{
		BlogResponseModel responseModel = new();

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

		string connectionString = _SqlConnectionStringBuilder.ConnectionString;
		SqlConnection connection = new(connectionString);

		connection.Open();

		string query = $@"UPDATE [dbo].[TBL_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

		SqlCommand cmd = new(query, connection);
		cmd.Parameters.AddWithValue("@BlogId", requestModel.BlogId);
		cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
		cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
		cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);

		int result = cmd.ExecuteNonQuery();

		connection.Close();

		string message = result > 0 ? "Updating successful." : "Updating failed.";

		responseModel.IsSuccessful = result > 0;
		responseModel.Message = message;
		responseModel.Data = result > 0 ? requestModel : null;
		return responseModel;
	}

	public BlogResponseModel DeleteBlog(string id)
	{
		string connectionString = _SqlConnectionStringBuilder.ConnectionString;
		SqlConnection connection = new(connectionString);

		connection.Open();

		SqlCommand cmd = new("delete from TBL_Blog where BlogId = @BlogId", connection);
		cmd.Parameters.AddWithValue("@BlogId", id);

		int result = cmd.ExecuteNonQuery();

		connection.Close();

		string message = result > 0 ? "Deleting successful." : "Deleting failed.";

		return new BlogResponseModel()
		{
			IsSuccessful = result > 0,
			Message = message
		};
	}
}
