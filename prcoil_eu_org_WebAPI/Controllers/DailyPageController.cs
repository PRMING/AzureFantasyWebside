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

    public class DailyPageController : ControllerBase
    {
        string _defultSelectTable = "daily_page";
        
        //插入---------------------------------------------------------------------------------------------------------------------
        //启用跨域
        [EnableCors("AnotherPolicy")]
        [HttpPost("DailyPageSet")]//!!!跨域必须指定路由模板不同名称
        public IActionResult DailyPageSet([FromBody] DailyPageData? userSetDailyData)
        {
            if (userSetDailyData != null)
            {
                string? from = userSetDailyData.From;
                string? fromWho = userSetDailyData.FromWho;
                string? hitokoto = userSetDailyData.Hitokoto;
                string? imageUrl = userSetDailyData.ImageUrl;
                
                MySqlService mySqlService = new MySqlService();//数据库对象
                
                // from是sql的关键字需要加上反引号
                mySqlService.MySqlInsertWebReg("from_where", "from_who", "hitokoto", "image_url", _defultSelectTable, from, fromWho, hitokoto, imageUrl);
                mySqlService.MySqlInsertOne("show_date",_defultSelectTable,$"{DateTime.Now.Year}年{DateTime.Now.Month}月{DateTime.Now.Day}日");
                return Ok(new { message = "完成" });
            }
            return Ok(new { message = "无数据" });
        }



        //查询---------------------------------------------------------------------------------------------------------------------
        //启用跨域
        [EnableCors("AnotherPolicy")]
        [HttpGet("DailyPageGet")] //!!!跨域必须指定路由模板不同名称
         public IActionResult DailyPageGet(string showDate)
         {
             MySqlService mySqlService = new MySqlService();
             
             return Ok(new
             {
                 hitokoto = mySqlService.MySqlSelect("hitokoto","show_date",showDate, _defultSelectTable),
                 from = mySqlService.MySqlSelect("from_where","show_date",showDate, _defultSelectTable),
                 fromWho = mySqlService.MySqlSelect("from_who","show_date",showDate, _defultSelectTable),
                 showTime = mySqlService.MySqlSelect("show_date","show_date",showDate, _defultSelectTable),
                 imageUrl = mySqlService.MySqlSelect("image_url","show_date",showDate, _defultSelectTable),
             });
         }

    }
}
