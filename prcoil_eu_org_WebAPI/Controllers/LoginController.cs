using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prcoil_eu_org_WebAPI.Controllers
{
    //路由是直接函数名字
    [Route("[action]")]
    //[Route("[controller]/[action]")]

    [ApiController]

    public class LoginController : ControllerBase
    {
        [HttpGet]
        public IActionResult UserLogin()
        {
            return Ok();
        }
    }
}
