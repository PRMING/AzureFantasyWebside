using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PrcoilWebAPI.Models;
using PrcoilWebAPI.Models.Data;

namespace PrcoilWebAPI.Controllers;

//路由是直接函数名字
[Route("")]
//[Route("[controller]/[action]")]
[ApiController]
public class LoginRegisterController : ControllerBase
{
    private readonly string _defultSelectTable = "web_users_data";


    //登录---------------------------------------------------------------------------------------------------------------------
    //启用跨域
    [EnableCors("AnotherPolicy")]
    [HttpPost("UserLogin")] //!!!跨域必须指定路由模板不同名称
    public IActionResult UserLogin([FromBody] UserLoginData? userLoginData)
    {
        if (userLoginData != null)
        {
            var cellphone = userLoginData.Cellphone;
            var password = userLoginData.Password;
            //string username = userLoginData.username;
            //string captcha = userLoginData.captcha;
            //string remember = userLoginData.remember;

            var mySqlService = new MySqlService(); //数据库对象

            //如果找不到账号:
            if ("DataNotFound" == mySqlService.MySqlSelect("password", "cellphone", cellphone, _defultSelectTable))
                return Ok(new { message = "未找到账户" });
            //如果找到账户:
            if (password == mySqlService.MySqlSelect("password", "cellphone", cellphone, _defultSelectTable))
            {
                //允许获取header中的"authorization"(token)
                HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "authorization");

                //token服务
                var tokenService = new TokenService();

                //设置token在响应头
                HttpContext.Response.Headers.Add("authorization", $"{tokenService.GenerateJwtToken(cellphone)}");

                return Ok(new { message = "已登录,Token已发送" });
            }
            //如果密码不对:

            return Ok(new { message = "密码错误" });
        }

        return Ok("服务器错误,注册功能异常");
    }


    //注册---------------------------------------------------------------------------------------------------------------------
    //启用跨域
    [EnableCors("AnotherPolicy")]
    [HttpPost("UserRegisher")] //!!!跨域必须指定路由模板不同名称
    public IActionResult UserRegisher([FromBody] UserRegisterData? userRegisterData)
    {
        if (userRegisterData != null)
        {
            var mySqlService = new MySqlService(); //数据库对象

            var cellphone = userRegisterData.Cellphone;
            var username = userRegisterData.Username;
            var password = userRegisterData.Password;
            //未设置email
            var email = "";

            //如果找不到账户:
            if ("DataNotFound" == mySqlService.MySqlSelect("password", "cellphone", cellphone, _defultSelectTable))
            {
                mySqlService.MySqlInsertWebReg("username", "email", "cellphone", "password", _defultSelectTable,
                    username, email, cellphone, password);

                return Ok(new { message = "已注册" });
            }
            //如果找的到账户

            if (mySqlService.MySqlSelect("password", "cellphone", cellphone, _defultSelectTable) != "DataNotFound")
                return Ok(new { message = "账户已被注册" });
            return Ok("服务器错误,注册功能异常");
        }

        return Ok("服务器错误,注册功能异常");
    }
}