namespace PrcoilWebMVC.Models.Data;

public class ReCaptchaData
{
    /// <summary>
    /// 
    /// </summary>
    public bool success { get;set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime challenge_ts { get;set; }

    /// <summary>
    /// 
    /// </summary>
    public string hostname { get;set; }

    /// <summary>
    /// 
    /// </summary>
    public double score { get; set; } = 999;

    /// <summary>
    /// 
    /// </summary>
    public string action { get;set; }
}