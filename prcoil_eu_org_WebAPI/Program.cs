using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace prcoil_eu_org_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //---------------------------------------------------------------------------------------------------------------------
            //跨域权限设置
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Policy1",
                    policy =>
                    {
                        policy.WithOrigins("http://example.com",
                                            "http://www.contoso.com")
                              .WithMethods("PUT", "DELETE", "GET");
                    });

                options.AddPolicy("AnotherPolicy",
                    policy =>
                    {
                        policy.WithOrigins("https://www.prcoil.eu.org",
                                           "http://127.0.0.1:5500",
                                           "https://testipv6.prcoil.eu.org")
                                            .AllowCredentials()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });

            // 在此处配置JWT身份验证
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "prcoil",
                        ValidateAudience = true,
                        ValidAudience = "webprcoil",
                        ValidateLifetime = true,
                        //key
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("amA9gMV2GQNhg4NAeJe5sBiclyvv7HQD9eGPr3y1CTPUmXJewqo1UMyW/ouxZLJdqDO8351LBKtD8+S7pvrZsw==")),
                        //ClockSkew = TimeSpan.Zero
                    };
                });

            //---------------------------------------------------------------------------------------------------------------------

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // 在这里配置其他服务-----------------------------------------------------------------------------------------------------

            //跨域
            app.UseCors();
            //token
            app.UseAuthentication();

            //---------------------------------------------------------------------------------------------------------------------

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}