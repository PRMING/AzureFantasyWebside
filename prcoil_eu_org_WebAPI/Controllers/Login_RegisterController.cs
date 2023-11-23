using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prcoil_eu_org_WebAPI.Models;
using prcoil_eu_org_WebAPI.Models.Data;

namespace prcoil_eu_org_WebAPI.Controllers
{
    //路由是直接函数名字
    [Route("")]
    //[Route("[controller]/[action]")]

    [ApiController]

    public class LoginRegisterController : ControllerBase
    {
        string _defultSelectTable = "web_users_data";

        

        //登录---------------------------------------------------------------------------------------------------------------------
        //启用跨域
        [EnableCors("AnotherPolicy")]
        [HttpPost("UserLogin")]//!!!跨域必须指定路由模板不同名称
        public IActionResult UserLogin([FromBody] UserLoginData? userLoginData)
        {
            if (userLoginData != null)
            {
                string? cellphone = userLoginData.Cellphone;
                string? password = userLoginData.Password;
                //string username = userLoginData.username;
                //string captcha = userLoginData.captcha;
                //string remember = userLoginData.remember;

                MySqlService mySqlService = new MySqlService();//数据库对象

                //如果找不到账号:
                if ("DataNotFound" == mySqlService.MySqlSelect("passworld", "cellphone", cellphone, _defultSelectTable)) 
                {
                    return Ok(new { message = "未找到账户" });
                }
                //如果找到账户:
                else if (password == mySqlService.MySqlSelect("passworld","cellphone", cellphone, _defultSelectTable))
                {
                    //允许获取header中的"authorization"(token)
                    HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "authorization");

                    //token服务
                    TokenService tokenService = new TokenService();

                    //设置token在响应头
                    HttpContext.Response.Headers.Add("authorization", $"{tokenService.GenerateJwtToken(cellphone)}");

                    return Ok("已登录,Token已发送");
                }
                //如果密码不对:
                else
                {
                    return Ok("密码错误");
                }
            }
            return Ok("服务器错误,注册功能异常");
        }



        //注册---------------------------------------------------------------------------------------------------------------------
        //启用跨域
        [EnableCors("AnotherPolicy")]
        [HttpPost("UserRegisher")]//!!!跨域必须指定路由模板不同名称
        public IActionResult UserRegisher([FromBody] UserRegisterData? userRegisterData)
        {
            if (userRegisterData != null)
            {
                MySqlService mySqlService = new MySqlService();//数据库对象

                string? cellphone = userRegisterData.Cellphone;
                string? username = userRegisterData.Username;
                string? password = userRegisterData.Password;
                //未设置email
                string? email = "";

                //如果找不到账户:
                if ("DataNotFound" == mySqlService.MySqlSelect("passworld", "cellphone", cellphone, _defultSelectTable))
                {
                    mySqlService.MySqlInsertWebReg("username", "email", "cellphone", "passworld", _defultSelectTable, username, email, cellphone, password);

                    return Ok(new { message = "已注册" });
                }
                //如果找的到账户
                else if (mySqlService.MySqlSelect("passworld", "cellphone", cellphone, _defultSelectTable) != "DataNotFound")
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
