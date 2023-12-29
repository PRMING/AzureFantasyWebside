/*
                   _ooOoo_
                  o8888888o
                  88" . "88
                  (| -_- |)
                  O\  =  /O
               ____/`---'\____
             .'  \\|     |//  `.
            /  \\|||  :  |||//  \
           /  _||||| -:- |||||-  \
           |   | \\\  -  /// |   |
           | \_|  ''\---/''  |   |
           \  .-\__  `-`  ___/-. /
         ___`. .'  /--.--\  `. . __
      ."" '<  `.___\_<|>_/___.'  >'"".
     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
     \  \ `-.   \_ __\ /__ _/   .-` /  /
======`-.____`-.___\_____/___.-`____.-'======
                   `=---='
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            佛祖保佑       永无BUG
*/

using Microsoft.AspNetCore.Authentication.Cookies;

namespace PrcoilWebMVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //---------------------------------------------------------------------------------------------------------------------
        //跨域权限设置
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Policy1",
                policy =>
                {
                    policy.WithOrigins("http://example.com",
                            "http://www.contoso.com")
                        .WithMethods("PUT", "DELETE", "GET");
                });

            options.AddPolicy("AnotherPolicy",
                policy =>
                {
                    policy.WithOrigins("https://www.prcoil.eu.org",
                            "http://127.0.0.1:5501",
                            "http://localhost:63342",
                            "https://testipv6.prcoil.eu.org",
                            "https://localhost:7190",
                            "https://aspnet.prcoil.eu.org")
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        // 在此处配置身份验证
        // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer(options =>
        //     {
        //         options.TokenValidationParameters = new TokenValidationParameters
        //         {
        //             ValidateIssuer = true,
        //             ValidIssuer = "prcoil", //发行人Issuer

        //             ValidateAudience = true,
        //             ValidAudience = "webprcoil", //订阅人Audience

        //             ValidateLifetime = true, //是否验证失效时间

        //             ValidateIssuerSigningKey = true, //是否验证SecurityKey
        //             //SecurityKey
        //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
        //                 "amA9gMV2GQNhg4NAeJe5sBiclyvv7HQD9eGPr3y1CTPUmXJewqo1UMyW/ouxZLJdqDO8351LBKtD8+S7pvrZsw==")),
        //             ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
        //             RequireExpirationTime = true
        //         };
        //     });

        //选择使用那种方式来身份验证
        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; //默认身份验证方案
            option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            option.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
        {
            option.LoginPath = "/LoginReg";//如果没有找到用户信息---身份验证失败--授权也失败了---就跳转到指定的Action
            option.AccessDeniedPath = "";//如果用户没有权限访问某个Action，就跳转到指定的Action
            option.LogoutPath = "/LoginReg/Logout";//退出登录的路径
            option.Cookie.HttpOnly = true;//设置Cookie的HttpOnly属性为true，防止XSS攻击
            option.Cookie.Name = "prcoil";//cookie的名称
            option.Cookie.Path = "/";//cookie的作用域
            option.Cookie.SameSite = SameSiteMode.Lax;//防止跨站请求伪造
            option.Cookie.MaxAge = TimeSpan.FromDays(30);//过期时间
            option.ExpireTimeSpan = TimeSpan.FromDays(30);//Cookie有效期
            option.SlidingExpiration = true;//是否在过期时间内，刷新过期时间
        });

        // Add services to the container.
        builder.Services.AddHttpClient();
        
        // 启用Swagger
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //---------------------------------------------------------------------------------------------------------------------

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // 启用Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // 在这里配置其他服务-----------------------------------------------------------------------------------------------------

        //跨域
        app.UseCors();
        //调用中间件：UseAuthentication（认证），必须在所有需要身份认证的中间件前调用，比如 UseAuthorization（授权）。
        app.UseAuthentication();

        //---------------------------------------------------------------------------------------------------------------------

        app.UseAuthorization();

        app.MapControllerRoute(
            "default",
            "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}