using DotNetBatch14AMO.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14AMO.MvcApp.Controllers;

public class BlogController : Controller
{
	private readonly IBlogService _blogService;

	public BlogController(IBlogService blogService)
	{
		_blogService = blogService;
	}

	[ActionName("Index")]
	public IActionResult BlogList()
	{
		var list = _blogService.GetBlogs();
		return View("BlogList", list);
	}

	[ActionName("Create")]
	public IActionResult CreateBlog()
	{
		return View("CreateBlog");
	}

	[HttpPost]
	[ActionName("Save")]
	public IActionResult SaveBlog(BlogModel requestModel)
	{
		List<string> errorList = new();

		if(string.IsNullOrEmpty(requestModel.BlogTitle))
		{
			errorList.Add("Blog Title is required.");
		}
		if(string.IsNullOrEmpty(requestModel.BlogAuthor))
		{
			errorList.Add("Blog Author is required.");
		}
		if (string.IsNullOrEmpty(requestModel.BlogContent))
		{
			errorList.Add("Blog Content is required.");
		}
		if(errorList.Count > 0)
		{
			ViewBag.IsValidationError = true;
			ViewBag.ValidationErrors = errorList;
			return View("CreateBlog", requestModel);
		}

		var responseModel = _blogService.CreateBlog(requestModel);

		return RedirectToAction("Index");
	}

	[ActionName("edit")]
	public IActionResult EditBlog(string id)
	{
		var blog = _blogService.GetBlog(id);

		return View("EditBlog", blog);
	}

	[HttpPost]
	[ActionName("update")]
	public IActionResult UpdateBlog(string id, BlogModel requestModel)
	{
		requestModel.BlogId = id;
		var responseModel = _blogService.UpdateBlog(requestModel);

		TempData["IsSuccess"] = responseModel.IsSuccessful;
		TempData["Message"] = responseModel.Message;

		return RedirectToAction("Index");
	}

	[ActionName("delete")]
	public IActionResult DeleteBlog(string id)
	{
		var responseModel = _blogService.DeleteBlog(id);
		TempData["IsSuccess"] = responseModel.IsSuccessful;
		TempData["Message"] = responseModel.Message;

		return RedirectToAction("Index");
	}
}
