using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prcoil_eu_org_WebAPI.Models;

namespace prcoil_eu_org_WebAPI.Controllers
{
    //路由是直接函数名字
    [Route("")]
    //[Route("[controller]/[action]")]

    [ApiController]

    public class Login_RegisterController : ControllerBase
    {
        //创建数据库类 传入路径
        SQLite sqlite = new SQLite("DataBase\\UsersData.db");
        //token服务
        TokenService tokenService = new TokenService();

        //登录---------------------------------------------------------------------------------------------------------------------
        //启用跨域
        [EnableCors("AnotherPolicy")]
        [HttpPost("UserLogin")]//!!!跨域必须指定路由模板不同名称
        public IActionResult UserLogin([FromBody] UserLoginData userLoginData)
        {
            //连接数据库 一定记得要写！！！不然要报错
            sqlite.ConnectToDatabase();

            if (userLoginData != null)
            {
                string cellphone = userLoginData.cellphone;
                string password = userLoginData.password;
                //string username = userLoginData.username;
                //string captcha = userLoginData.captcha;
                //string remember = userLoginData.remember;

                //如果找不到账号:
                if ("DataNotFound" == sqlite.WebDataSelect("Passworld", "CellPhone", cellphone)) 
                {
                    return Ok(new { message = "未找到账户" });
                }
                //如果找到账户:
                else if (password == sqlite.WebDataSelect("Passworld","CellPhone", cellphone))
                {
                    //允许获取header中的"authorization"(token)
                    HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "authorization");
                    //设置token在响应头
                    HttpContext.Response.Headers.Add("authorization", $"{tokenService.GenerateJwtToken(sqlite.WebDataSelect("UserName", "CellPhone", cellphone))}");

                    return Ok(new { message = "已登录,Token已发送" });
                }
                //如果密码不对:
                else
                {
                    return Ok(new { message = "密码错误" });
                }
            }

            return Ok("服务器错误,注册功能异常");
        }



        //注册---------------------------------------------------------------------------------------------------------------------
        //启用跨域
        [EnableCors("AnotherPolicy")]
        [HttpPost("UserRegisher")]//!!!跨域必须指定路由模板不同名称
        public IActionResult UserRegisher([FromBody] UserRegisterData userRegisterData)
        {
            //连接数据库 一定记得要写！！！不然要报错
            sqlite.ConnectToDatabase();

            if (userRegisterData != null)
            {
                string cellphone = userRegisterData.cellphone;
                string username = userRegisterData.username;
                string password = userRegisterData.password;
                //未设置email
                string email = "";

                //如果找不到账户:
                if ("DataNotFound" == sqlite.WebDataSelect("Passworld", "CellPhone", cellphone))
                {
                    sqlite.FillTableRegister(email, cellphone, password, username);

                    return Ok(new { message = "已注册" });
                }
                //如果找的到账户
                else if (sqlite.WebDataSelect("Passworld", "CellPhone", cellphone) != "DataNotFound")
                {
                    return Ok(new { message = "账户已被注册" });
                }
                else
                {
                    return Ok("服务器错误,注册功能异常");
                }
            }

            return Ok("服务器错误,注册功能异常");
        }
    }
}
