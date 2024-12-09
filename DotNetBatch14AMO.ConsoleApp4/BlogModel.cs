using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14AMO.ConsoleApp4;

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
	public BlogModel Data { get; set; }
}

public class BlogListResponseModel
{
	public bool IsSuccessful { get; set; }
	public string Message { get; set; }
	public List<BlogModel> Data { get; set; }
}