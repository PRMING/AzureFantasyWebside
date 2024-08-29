using Microsoft.AspNetCore.Mvc;

namespace AzureFantasy_Web.Controllers.view;

public class BlogController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}