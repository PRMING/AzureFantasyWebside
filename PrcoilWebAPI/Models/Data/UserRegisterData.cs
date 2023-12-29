namespace PrcoilWebAPI.Models.Data;

public class UserRegisterData
{
    public string? Username { get; set; }
    public string? Cellphone { get; set; }
    public string? Password { get; set; }


    public string? Vercode { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Agreement { get; set; }
}