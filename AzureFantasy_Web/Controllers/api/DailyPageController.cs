using Microsoft.AspNetCore.Mvc;
using AzureFantasy_Web.Models;

namespace AzureFantasy_Web.Controllers.api;

//路由是直接函数名字
[Route("")]
//[Route("[controller]/[action]")]
[ApiController]
public class DailyPageController : ControllerBase
{
    private const string DefaultSelectTable = "daily_page";

    //查询---------------------------------------------------------------------------------------------------------------------
    //启用跨域
    // [EnableCors("AnotherPolicy")]
    [HttpGet("DailyPageGet")] //!!!跨域必须指定路由模板不同名称
    public IActionResult DailyPageGet(string showDate)
    {
        var mySqlService = new MySqlService();

        return Ok(new
        {
            hitokoto = mySqlService.MySqlSelect("hitokoto", "show_date", showDate, DefaultSelectTable),
            from = mySqlService.MySqlSelect("from_where", "show_date", showDate, DefaultSelectTable),
            fromWho = mySqlService.MySqlSelect("from_who", "show_date", showDate, DefaultSelectTable),
            showTime = mySqlService.MySqlSelect("show_date", "show_date", showDate, DefaultSelectTable),
            imageUrl = mySqlService.MySqlSelect("image_url", "show_date", showDate, DefaultSelectTable)
        });
    }
}