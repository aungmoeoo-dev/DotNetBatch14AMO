namespace DotNetBatch14AMO.RestApi.Features.Blog;

public class BlogModel
{
	public string? BlogId { get; set; }
	public string? BlogTitle { get; set; }
	public string? BlogAuthor { get; set; }
	public string? BlogContent { get; set; }
}

public class BlogResponseModel
{
	public bool IsSuccessful { get; set; }
	public string Message { get; set; }
}