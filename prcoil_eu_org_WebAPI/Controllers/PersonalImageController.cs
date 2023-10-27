using Microsoft.AspNetCore.Mvc;

namespace prcoil_eu_org_WebAPI.Controllers
{
    //路由是直接函数名字
    [Route("[action]")]
    //[Route("[controller]/[action]")]

    [ApiController]

    public class PersonalImageController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetPersonalImage(string Name)
        {
            return Ok($"https://images.prcoil.eu.org/images/PersonalImage/{Name}.jpg");
        }
    }
}
