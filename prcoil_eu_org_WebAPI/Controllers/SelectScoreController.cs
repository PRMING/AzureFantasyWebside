using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prcoil_eu_org_WebAPI.Models;

namespace prcoil_eu_org_WebAPI.Controllers;

//路由是直接函数名字
[Route("")]
//[Route("[controller]/[action]")]
[ApiController]
public class SelectScoreController : ControllerBase
{
    //JWT身份验证(谁要就在谁前面加)
    // [Authorize]
    [EnableCors("AnotherPolicy")]
    [HttpGet("GetStudentData")]
    public IActionResult GetStudentData(string? name)
    {
        var defultSelectTable = "20231127";

        var mySqlService = new MySqlService("47.76.176.11","nzzx","nzzx", "6ebKpT2J5SjspXhZ");

        // 创建一个包含所需值的匿名对象
        var studentData = new
        {
            ban = mySqlService.MySqlSelect("ban", "name", name, defultSelectTable),
            name = mySqlService.MySqlSelect("name", "name", name, defultSelectTable),
            kaohao = mySqlService.MySqlSelect("kaohao", "name", name, defultSelectTable),
            zongfen = mySqlService.MySqlSelect("zongfen", "name", name, defultSelectTable),
            fufenzongfen = mySqlService.MySqlSelect("fufenzongfen", "name", name, defultSelectTable),
            zongfenpaiming = mySqlService.MySqlSelect("zongfenpaiming", "name", name, defultSelectTable),
            banjipaiming = mySqlService.MySqlSelect("banjipaiming", "name", name, defultSelectTable),
            yuwen = mySqlService.MySqlSelect("yuwen", "name", name, defultSelectTable),
            yuwenpaiming = mySqlService.MySqlSelect("yuwenpaiming", "name", name, defultSelectTable),
            shuxue = mySqlService.MySqlSelect("shuxue", "name", name, defultSelectTable),
            shuxuepaiming = mySqlService.MySqlSelect("shuxuepaiming", "name", name, defultSelectTable),
            yingyu = mySqlService.MySqlSelect("yingyu", "name", name, defultSelectTable),
            yingyupaiming = mySqlService.MySqlSelect("yingyupaiming", "name", name, defultSelectTable),
            wuli = mySqlService.MySqlSelect("wuli", "name", name, defultSelectTable),
            wulipaiming = mySqlService.MySqlSelect("wulipaiming", "name", name, defultSelectTable),
            lishi = mySqlService.MySqlSelect("lishi", "name", name, defultSelectTable),
            lishipaiming = mySqlService.MySqlSelect("lishipaiming", "name", name, defultSelectTable),
            huaxueyuanshi = mySqlService.MySqlSelect("huaxueyuanshi", "name", name, defultSelectTable),
            huaxuefufen = mySqlService.MySqlSelect("huaxuefufen", "name", name, defultSelectTable),
            huaxuepaiming = mySqlService.MySqlSelect("huaxuepaiming", "name", name, defultSelectTable),
            shengwuyuanshi = mySqlService.MySqlSelect("shengwuyuanshi", "name", name, defultSelectTable),
            shengwufufen = mySqlService.MySqlSelect("shengwufufen", "name", name, defultSelectTable),
            shengwupaiming = mySqlService.MySqlSelect("shengwupaiming", "name", name, defultSelectTable),
            zhengzhiyuanshi = mySqlService.MySqlSelect("zhengzhiyuanshi", "name", name, defultSelectTable),
            zhengzhifufen = mySqlService.MySqlSelect("zhengzhifufen", "name", name, defultSelectTable),
            zhengzhipaiming = mySqlService.MySqlSelect("zhengzhipaiming", "name", name, defultSelectTable),
            diliyuanshi = mySqlService.MySqlSelect("diliyuanshi", "name", name, defultSelectTable),
            dilifufen = mySqlService.MySqlSelect("dilifufen", "name", name, defultSelectTable),
            dilipaiming = mySqlService.MySqlSelect("dilipaiming", "name", name, defultSelectTable)
        };

        return Ok(studentData);
    }
}