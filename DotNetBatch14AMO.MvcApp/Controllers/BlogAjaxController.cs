using DotNetBatch14AMO.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14AMO.MvcApp.Controllers
{
	public class BlogAjaxController : Controller
	{
		private readonly IBlogService _blogService;
		public BlogAjaxController(IBlogService blogService)
		{
			_blogService = blogService;
		}

		public IActionResult Index()
		{
			return View("BlogList");
		}

		public IActionResult GetBlogs()
		{
			var list = _blogService.GetBlogs();

			return Json(list);
		}

		[ActionName("create")]
		public IActionResult CreateBlog()
		{
			return View("CreateBlog");
		}

		[HttpPost]
		[ActionName("save")]
		public IActionResult SaveBlog(BlogModel requestModel)
		{
			var responseModel = _blogService.CreateBlog(requestModel!);

			return Json(responseModel);
		}

		[HttpPost]
		[ActionName("delete")]
		public IActionResult DeleteBlog(BlogModel requestModel)
		{
			var responseModel = _blogService.DeleteBlog(requestModel.BlogId!);

			return Json(responseModel);
		}
	}
}
