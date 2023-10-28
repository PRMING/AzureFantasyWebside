namespace prcoil_eu_org_WebAPI.Models
{
    public class UserLoginData
    {
        public string username { get; set; }
        public string password { get; set; }
        public string captcha { get; set; }
        public string remember { get; set; }
    }
}
