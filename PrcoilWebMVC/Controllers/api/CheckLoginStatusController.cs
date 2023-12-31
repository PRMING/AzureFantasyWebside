using Microsoft.AspNetCore.Mvc;

namespace PrcoilWebMVC.Controllers.api;

//路由是直接函数名字
[Route("")]
//[Route("[controller]/[action]")]
[ApiController]
public class CheckLoginStatusController : ControllerBase
{
    //启用跨域
    // [EnableCors("AnotherPolicy")]
    [HttpGet("CheckLoginStatus")] //!!!跨域必须指定路由模板不同名称
    public IActionResult CheckLoginStatus()
    {
        if (Request.Cookies.ContainsKey("prcoil"))
        {
            // 用户已登录
            return Ok(new { loggedIn = true });
        }
        else
        {
            // 用户未登录
            return Ok(new { loggedIn = false });
        }
    }
}