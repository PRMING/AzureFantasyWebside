using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PrcoilWebMVC.Controllers.api;

//路由是直接函数名字
[Route("")]
//[Route("[controller]/[action]")]
[ApiController]
public class ImageController : ControllerBase
{
    [EnableCors("AnotherPolicy")]
    [HttpGet("GetPersonalImage")]
    public IActionResult GetPersonalImage(string name)
    {
        return Ok($"https://images.prcoil.eu.org/images/PersonalImage/{name}.jpg");
    }
    
    [HttpGet("GetUserImage")]
    public IActionResult UserImage(string id)
    {
        return Ok($"https://images.prcoil.eu.org/images/UserImage/{id}.jpg");
    }
}