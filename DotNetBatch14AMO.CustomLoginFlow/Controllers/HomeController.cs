using Microsoft.AspNetCore.Mvc;

namespace CustomLoginFlow.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			// Check if the user is authenticated by verifying the session
			var email = HttpContext.Session.GetString("email");
			if (string.IsNullOrEmpty(email))
			{
				// If not authenticated, redirect to the login page
				return RedirectToAction("Index", "SignIn");
			}

			// If authenticated, display the home page
			return View();
		}
	}
}