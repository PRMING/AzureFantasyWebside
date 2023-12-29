using Microsoft.AspNetCore.Mvc;

namespace PrcoilWebMVC.Controllers.view;

public class NotLoggedInController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}