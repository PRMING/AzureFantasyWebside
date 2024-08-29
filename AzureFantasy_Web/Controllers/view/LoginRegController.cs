using Microsoft.AspNetCore.Mvc;

namespace AzureFantasy_Web.Controllers.view
{
    public class LoginRegController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
