using Microsoft.AspNetCore.Cors;
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
        //token服务
        TokenService tokenService = new TokenService();

        //启用跨域
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
                    // Set the token in the response header
                    Response.Headers.Add("Authorization", $"Bearer {tokenService.GenerateJwtToken(userLoginData.username)}");

                    return Ok(new { message = "Token sent in the response header" });
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
