using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prcoil_eu_org_WebAPI.Models;

namespace prcoil_eu_org_WebAPI.Controllers
{
    //路由是直接函数名字
    [Route("[action]")]
    //[Route("[controller]/[action]")]

    [ApiController]

    public class LoginController : ControllerBase
    {
        //创建数据库类 传入路径
        SQLite sqlite = new SQLite("DataBase\\UsersData.db");

        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public IActionResult UserLogin([FromBody] UserLoginData userLoginData)
        {
            //连接数据库 一定记得要写！！！不然要报错
            sqlite.connectToDatabase();

            if (userLoginData != null)
            {
                string username = userLoginData.username;
                string password = userLoginData.password;
                string captcha = userLoginData.captcha;
                string remember = userLoginData.remember;

                if (userLoginData.password == sqlite.webLoginSelect("Passworld","PhoneNumber", userLoginData.username))
                {
                    return Ok("登录成功");
                }
                else
                {
                    return Ok("登录失败");
                }
            }

            return Ok("Invalid JSON data");
        }
    }
}
