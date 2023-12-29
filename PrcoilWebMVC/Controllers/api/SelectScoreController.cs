using Microsoft.AspNetCore.Mvc;
using PrcoilWebMVC.Models;

namespace PrcoilWebMVC.Controllers.api;

//路由是直接函数名字
[Route("")]
//[Route("[controller]/[action]")]
[ApiController]
public class SelectScoreController : ControllerBase
{
    private const string DefaultSelectTable = "senior_second_shang_half";
    
    // [Authorize]
    // [EnableCors("AnotherPolicy")]
    [HttpGet("GetStudentData")]
    public IActionResult GetStudentData(string? name)
    {
        var mySqlService = new MySqlService();

        // 创建一个包含所需值的匿名对象
        var studentData = new
        {
            Class = mySqlService.MySqlSelect("class", "name", name, DefaultSelectTable),
            Name = mySqlService.MySqlSelect("name", "name", name, DefaultSelectTable),
            Num = mySqlService.MySqlSelect("num", "name", name, DefaultSelectTable),
            Score = mySqlService.MySqlSelect("score", "name", name, DefaultSelectTable),
            Assignscore = mySqlService.MySqlSelect("assignscore", "name", name, DefaultSelectTable),
            Graderanking = mySqlService.MySqlSelect("graderanking", "name", name, DefaultSelectTable),
            Classranking = mySqlService.MySqlSelect("classranking", "name", name, DefaultSelectTable),
            Chinese = mySqlService.MySqlSelect("chinese", "name", name, DefaultSelectTable),
            Chineseranking = mySqlService.MySqlSelect("chineseranking", "name", name, DefaultSelectTable),
            Math = mySqlService.MySqlSelect("math", "name", name, DefaultSelectTable),
            Mathranking = mySqlService.MySqlSelect("mathranking", "name", name, DefaultSelectTable),
            English = mySqlService.MySqlSelect("english", "name", name, DefaultSelectTable),
            Englishranking = mySqlService.MySqlSelect("englishranking", "name", name, DefaultSelectTable),
            Physics = mySqlService.MySqlSelect("physics", "name", name, DefaultSelectTable),
            Physicsranking = mySqlService.MySqlSelect("physicsranking", "name", name, DefaultSelectTable),
            History = mySqlService.MySqlSelect("history", "name", name, DefaultSelectTable),
            Historyranking = mySqlService.MySqlSelect("historyranking", "name", name, DefaultSelectTable),
            Chemistry = mySqlService.MySqlSelect("chemistry", "name", name, DefaultSelectTable),
            Assignchemistry = mySqlService.MySqlSelect("assignchemistry", "name", name, DefaultSelectTable),
            Chemistryranking = mySqlService.MySqlSelect("chemistryranking", "name", name, DefaultSelectTable),
            Organism = mySqlService.MySqlSelect("organism", "name", name, DefaultSelectTable),
            Assignorganism = mySqlService.MySqlSelect("assignorganism", "name", name, DefaultSelectTable),
            Organismranking = mySqlService.MySqlSelect("organismranking", "name", name, DefaultSelectTable),
            Politics = mySqlService.MySqlSelect("politics", "name", name, DefaultSelectTable),
            Assignpolitics = mySqlService.MySqlSelect("assignpolitics", "name", name, DefaultSelectTable),
            Politicsranking = mySqlService.MySqlSelect("politicsranking", "name", name, DefaultSelectTable),
            Geography = mySqlService.MySqlSelect("geography", "name", name, DefaultSelectTable),
            Assigngeography = mySqlService.MySqlSelect("assigngeography", "name", name, DefaultSelectTable),
            Geographyranking = mySqlService.MySqlSelect("geographyranking", "name", name, DefaultSelectTable)
        };

        return Ok(studentData);
    }
}