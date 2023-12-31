using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrcoilWebMVC.Models;

namespace PrcoilWebMVC.Controllers.api;

[Authorize]
[Route("")]
[ApiController]
public class WebUsersInfoController : ControllerBase
{
    private const string DefaultSelectTable = "web_users_data";

    //查询---------------------------------------------------------------------------------------------------------------------
    // [EnableCors("AnotherPolicy")]
    [HttpGet("GetWebUsersInfo")] //!!!跨域必须指定路由模板不同名称
    public IActionResult GetWebUsersInfo()
    {
        // 获取返回的JWT的内容
        var phone = User.FindFirst(ClaimTypes.MobilePhone)?.Value;
        // var userName = User.FindFirst(ClaimTypes.Name)?.Value;

        var mySqlService = new MySqlService();

        return Ok(new
        {
            id = mySqlService.MySqlSelect("id", "cellphone", phone, DefaultSelectTable),
            username = mySqlService.MySqlSelect("username", "cellphone", phone, DefaultSelectTable),
            email = mySqlService.MySqlSelect("email", "cellphone", phone, DefaultSelectTable),
            password = mySqlService.MySqlSelect("password", "cellphone", phone, DefaultSelectTable),
            create_time = mySqlService.MySqlSelect("create_time", "cellphone", phone, DefaultSelectTable),
            avatar = mySqlService.MySqlSelect("avatar", "cellphone", phone, DefaultSelectTable)
        });
    }
}