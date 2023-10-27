using Microsoft.AspNetCore.Mvc;
using prcoil_eu_org_WebAPI;

namespace prcoil_eu_org_WebAPI.Controllers
{
    //路由是直接函数名字
    [Route("[action]")]
    //[Route("[controller]/[action]")]

    [ApiController]

    public class SelectScoreController : ControllerBase
    {
        //创建数据库类 传入路径
        SQLite sqlite = new SQLite("DataBase\\NZZX2022Score.db");

        [HttpGet]
        public IActionResult GetStudentData(string Name)
        {
            //sqlite.Select($"SELECT * FROM SeniorTwo1 WHERE Name = '{Name}'");

            //连接数据库
            sqlite.connectToDatabase();

            // 创建一个包含所需值的匿名对象
            var studentData = new
            {
                Class = sqlite.Select(Name, "Class"),
                Name = sqlite.Select(Name, "Name"),
                Num = sqlite.Select(Name, "Num"),
                Score = sqlite.Select(Name, "Score"),
                AssignScore = sqlite.Select(Name, "AssignScore"),
                GradeRanking = sqlite.Select(Name, "GradeRanking"),
                ClassRanking = sqlite.Select(Name, "ClassRanking"),
                Chinese = sqlite.Select(Name, "Chinese"),
                ChineseRanking = sqlite.Select(Name, "ChineseRanking"),
                Math = sqlite.Select(Name, "Math"),
                MathRanking = sqlite.Select(Name, "MathRanking"),
                English = sqlite.Select(Name, "English"),
                EnglishRanking = sqlite.Select(Name, "EnglishRanking"),
                Physics = sqlite.Select(Name, "Physics"),
                PhysicsRanking = sqlite.Select(Name, "PhysicsRanking"),
                History = sqlite.Select(Name, "History"),
                HistoryRanking = sqlite.Select(Name, "HistoryRanking"),
                Chemistry = sqlite.Select(Name, "Chemistry"),
                AssignChemistry = sqlite.Select(Name, "AssignChemistry"),
                ChemistryRanking = sqlite.Select(Name, "ChemistryRanking"),
                Organism = sqlite.Select(Name, "Organism"),
                AssignOrganism = sqlite.Select(Name, "AssignOrganism"),
                OrganismRanking = sqlite.Select(Name, "OrganismRanking"),
                Politics = sqlite.Select(Name, "Politics"),
                AssignPolitics = sqlite.Select(Name, "AssignPolitics"),
                PoliticsRanking = sqlite.Select(Name, "PoliticsRanking"),
                Geography = sqlite.Select(Name, "Geography"),
                AssignGeography = sqlite.Select(Name, "AssignGeography"),
                GeographyRanking = sqlite.Select(Name, "GeographyRanking"),
            };

            return Ok(studentData);
        }
    }
}
