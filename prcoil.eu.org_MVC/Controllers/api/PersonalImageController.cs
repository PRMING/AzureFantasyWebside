using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace prcoil.eu.org_MVC.Controllers.api;

//路由是直接函数名字
[Route("")]
//[Route("[controller]/[action]")]
[ApiController]
public class PersonalImageController : ControllerBase
{
    [EnableCors("AnotherPolicy")]
    [HttpGet("GetPersonalImage")]
    public IActionResult GetPersonalImage(string name)
    {
        return Ok($"https://images.prcoil.eu.org/images/PersonalImage/{name}.jpg");
    }
}