using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    public string GenerateJwtToken(string _Username)
    {
        string secretKey = "amA9gMV2GQNhg4NAeJe5sBiclyvv7HQD9eGPr3y1CTPUmXJewqo1UMyW/ouxZLJdqDO8351LBKtD8+S7pvrZsw==";
        string issuer = "prcoil";
        string audience = "webprcoil";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, _Username),
            //new Claim(ClaimTypes.Email, _Email),
            new Claim("customClaimKey", "customClaimValue"), // 添加自定义声明
            // 添加其他所需的自定义声明
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
