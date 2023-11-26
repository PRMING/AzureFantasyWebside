using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prcoil.eu.org_MVC.Models;

namespace prcoil.eu.org_MVC.Controllers.api;

//路由是直接函数名字
[Route("")]
//[Route("[controller]/[action]")]
[ApiController]
public class SelectScoreController : ControllerBase
{
    //JWT身份验证(谁要就在谁前面加)
    // [Authorize]
    // [EnableCors("AnotherPolicy")]
    [HttpGet("GetStudentData")]
    public IActionResult GetStudentData(string? name)
    {
        var defultSelectTable = "senior_second_month1";

        var mySqlService = new MySqlService();

        // 创建一个包含所需值的匿名对象
        var studentData = new
        {
            Class = mySqlService.MySqlSelect("class", "name", name, defultSelectTable),
            Name = mySqlService.MySqlSelect("name", "name", name, defultSelectTable),
            Num = mySqlService.MySqlSelect("num", "name", name, defultSelectTable),
            Score = mySqlService.MySqlSelect("score", "name", name, defultSelectTable),
            Assignscore = mySqlService.MySqlSelect("assignscore", "name", name, defultSelectTable),
            Graderanking = mySqlService.MySqlSelect("graderanking", "name", name, defultSelectTable),
            Classranking = mySqlService.MySqlSelect("classranking", "name", name, defultSelectTable),
            Chinese = mySqlService.MySqlSelect("chinese", "name", name, defultSelectTable),
            Chineseranking = mySqlService.MySqlSelect("chineseranking", "name", name, defultSelectTable),
            Math = mySqlService.MySqlSelect("math", "name", name, defultSelectTable),
            Mathranking = mySqlService.MySqlSelect("mathranking", "name", name, defultSelectTable),
            English = mySqlService.MySqlSelect("english", "name", name, defultSelectTable),
            Englishranking = mySqlService.MySqlSelect("englishranking", "name", name, defultSelectTable),
            Physics = mySqlService.MySqlSelect("physics", "name", name, defultSelectTable),
            Physicsranking = mySqlService.MySqlSelect("physicsranking", "name", name, defultSelectTable),
            History = mySqlService.MySqlSelect("history", "name", name, defultSelectTable),
            Historyranking = mySqlService.MySqlSelect("historyranking", "name", name, defultSelectTable),
            Chemistry = mySqlService.MySqlSelect("chemistry", "name", name, defultSelectTable),
            Assignchemistry = mySqlService.MySqlSelect("assignchemistry", "name", name, defultSelectTable),
            Chemistryranking = mySqlService.MySqlSelect("chemistryranking", "name", name, defultSelectTable),
            Organism = mySqlService.MySqlSelect("organism", "name", name, defultSelectTable),
            Assignorganism = mySqlService.MySqlSelect("assignorganism", "name", name, defultSelectTable),
            Organismranking = mySqlService.MySqlSelect("organismranking", "name", name, defultSelectTable),
            Politics = mySqlService.MySqlSelect("politics", "name", name, defultSelectTable),
            Assignpolitics = mySqlService.MySqlSelect("assignpolitics", "name", name, defultSelectTable),
            Politicsranking = mySqlService.MySqlSelect("politicsranking", "name", name, defultSelectTable),
            Geography = mySqlService.MySqlSelect("geography", "name", name, defultSelectTable),
            Assigngeography = mySqlService.MySqlSelect("assigngeography", "name", name, defultSelectTable),
            Geographyranking = mySqlService.MySqlSelect("geographyranking", "name", name, defultSelectTable)
        };

        return Ok(studentData);
    }
}