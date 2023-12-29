namespace PrcoilWebMVC.Models.Data;

public class UserRegisterData
{
    public string? Username { get; set; }
    public string? Cellphone { get; set; }
    public string? Password { get; set; }

    public string? RecaptchaToken { get; set; }
}