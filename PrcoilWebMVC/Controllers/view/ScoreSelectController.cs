using Microsoft.AspNetCore.Mvc;

namespace AzureFantasy_Web.Controllers.view
{
    public class ScoreSelectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
