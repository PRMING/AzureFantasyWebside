using Microsoft.AspNetCore.Mvc;
using PrcoilWebMVC.Models;

namespace PrcoilWebMVC.Controllers.api;

[Route("[action]")]
[ApiController]
public class SelectScoreController : ControllerBase
{
    private const string DefaultSelectTable = "senior_second_shang_half";
    
    // [Authorize]
    // [EnableCors("AnotherPolicy")]
    [HttpGet("{name}&{reCaptchaToken}&{ip}")]
    public IActionResult GetStudentData(string? name, string? reCaptchaToken, string? ip)
    { 
        if (reCaptchaToken != null)
        {
            //reCaptcha验证:
            // 验证秘钥
            var secret1 = "6LfZDdooAAAAAMFxgUk8zREAhpssteFrlgxRc7GC";
            // 示例用法
            HttpClientServer httpClientServer = new HttpClientServer();

            // 示例 API URL 和数据对象
            string apiUrl = "https://www.recaptcha.net/recaptcha/api/siteverify";
            var formData = new Dictionary<string, string>
            {
                { "response", reCaptchaToken },
                { "secret", secret1 }
            };

            // 发送 HTTP POST 请求

            double result = httpClientServer.SendPostRequest(apiUrl, formData);
            //double result = 0.8;

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

            var mySqlService = new MySqlService();
            
            // 创建一个包含所需值的匿名对象
            if (mySqlService.MySqlSelect("name", "name", name, DefaultSelectTable) == "DataNotFound")
            {
                return Ok(new { message = "未找到该学生" });
            }

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
                Geographyranking = mySqlService.MySqlSelect("geographyranking", "name", name, DefaultSelectTable),
                avatar = $"https://prcoilserver-1301443616.cos.ap-chongqing.myqcloud.com/PersonalAvatar/{name}.jpg_img"
            };
            
            mySqlService.MySqlInsert2("ip_address","search_who","web_student_info_records",ip,name);
            
            return Ok(studentData);
        }
        
        return Ok("服务器错误,注册功能异常");
    }
}