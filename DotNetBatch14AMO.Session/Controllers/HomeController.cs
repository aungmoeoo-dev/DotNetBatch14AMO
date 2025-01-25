using DotNetBatch14AMO.Session.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotNetBatch14AMO.Session.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult SetSession()
		{
			HttpContext.Session.SetString("Username", "JohnDoe");
			return Content("Session value set!");
		}

		// Action to retrieve a session value
		public IActionResult GetSession()
		{
			var username = HttpContext.Session.GetString("Username");
			if (string.IsNullOrEmpty(username))
			{
				return Content("No session value found.");
			}
			return Content($"Hello, {username}!");
		}
	}
}
