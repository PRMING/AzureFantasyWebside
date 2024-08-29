using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzureFantasy_Web.Controllers.view;

public class MyAccountController : Controller
{

    // GET
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult PersonalInfo()
    {
        return View();
    }
}