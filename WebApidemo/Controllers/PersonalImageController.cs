using Microsoft.AspNetCore.Mvc;

namespace prcoil_eu_org.Controllers
{
    [ApiController]
    [Route("[action]")]
    //[Route("[controller]/[action]")]
    public class PersonalImageController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetPersonalImage(string Name)
        {
            return Ok($"https://images.prcoil.eu.org/images/PersonalImage/{Name}.jpg");
        }
    }
}
