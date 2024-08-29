using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AzureFantasy_Web.Models;
using AzureFantasy_Web.Models.Data;
using System.Security.Claims;

namespace AzureFantasy_Web.Controllers.api;

//路由是直接函数名字
[Route("")]
//[Route("[controller]/[action]")]
[ApiController]
public class LoginRegisterController : ControllerBase
{
    private const string DefaultSelectTable = "web_users_data";


    //登录---------------------------------------------------------------------------------------------------------------------
    //启用跨域
    [EnableCors("AnotherPolicy")]
    [HttpPost("UserLogin")] //!!!跨域必须指定路由模板不同名称
    public IActionResult UserLogin([FromBody] UserLoginData? userLoginData)
    {
        if (userLoginData == null)
            return Ok("userLoginData不能为null");

        if (userLoginData.Cellphone == null)
            return Ok(new { message = "phone不能为null" });

        var cellphone = userLoginData.Cellphone;
        var password = userLoginData.Password;
        //string username = userLoginData.username;
        //string captcha = userLoginData.captcha;
        //string remember = userLoginData.remember;

        var mySqlService = new MySqlService(); //数据库对象

        //如果找不到账号:
        if ("DataNotFound" == mySqlService.MySqlSelect("password", "cellphone", cellphone, DefaultSelectTable))
            return Ok(new { message = "未找到账户" });
        //如果密码不对:
        if (password != mySqlService.MySqlSelect("password", "cellphone", cellphone, DefaultSelectTable))
            return Ok(new { message = "密码错误" });

        var claims = new List<Claim>()//身份验证信息
        {
            new Claim(ClaimTypes.MobilePhone,cellphone),
        };

        var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));
        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
        {
            ExpiresUtc = DateTime.UtcNow.AddDays(31),//过期时间：31天
        }).Wait();
        // var user = HttpContext.User;

        return Ok(new { message = "已登录,Token已发送", redirectUrl = "/MyAccount" });
    }
    //注册---------------------------------------------------------------------------------------------------------------------
    //启用跨域
    [EnableCors("AnotherPolicy")]
    [HttpPost("UserRegister")] //!!!跨域必须指定路由模板不同名称
    public IActionResult UserRegister([FromBody] UserRegisterData? userRegisterData)
    {
        if (userRegisterData != null)
        {
            var mySqlService = new MySqlService(); //数据库对象

            var cellphone = userRegisterData.Cellphone;
            var username = userRegisterData.Username;
            var password = userRegisterData.Password;
            var recaptchaToken = userRegisterData.RecaptchaToken;
            //未设置email
            var email = "";

            //reCaptcha验证:
            // 验证秘钥
            var secret1 = "6LfZDdooAAAAAMFxgUk8zREAhpssteFrlgxRc7GC";
            // 示例用法
            HttpClientServer httpClientServer = new HttpClientServer();

            // 示例 API URL 和数据对象
            string apiUrl = "https://www.recaptcha.net/recaptcha/api/siteverify";
            var formData = new Dictionary<string, string>
            {
                { "response", recaptchaToken },
                { "secret", secret1 }
            };

            // 发送 HTTP POST 请求

            double result = httpClientServer.SendPostRequest(apiUrl, formData);
            //double result = 0.2;

            Console.WriteLine($"Response: {result}");

            if (result == 999)
            {
                return Ok(new { message = "验证服务器错误" });
            }

            // 处理结果
            if (result < 0.5)
            {
                return Ok(new { message = "验证不通过" });
            }
            // else
            // {
            //     Console.WriteLine("Failed to send HTTP POST request.");
            // }


            //如果找不到账户:
            if ("DataNotFound" == mySqlService.MySqlSelect("password", "cellphone", cellphone, DefaultSelectTable))
            {
                mySqlService.MySqlInsertWebReg("username", "email", "cellphone", "password", DefaultSelectTable,
                    username, email, cellphone, password);

                return Ok(new { message = "已注册" });
            }
            //如果找的到账户

            if (mySqlService.MySqlSelect("password", "cellphone", cellphone, DefaultSelectTable) != "DataNotFound")
                return Ok(new { message = "账户已被注册" });
            return Ok("服务器错误,注册功能异常");
        }
        return Ok("服务器错误,注册功能异常");
    }

    //登出---------------------------------------------------------------------------------------------------------------------
    [HttpGet("UserLogout")] //!!!跨域必须指定路由模板不同名称
    public IActionResult UserLogout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        return Ok(new { message = "已登出", redirectUrl = "/" });
    }
}