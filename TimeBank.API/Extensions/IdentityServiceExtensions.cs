using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using TimeBank.API.Services;
using TimeBank.Repository;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //var jwtSecret = config.GetSection("JwtSecurityKey").Get<string>();
            var jwtSecret = config["JwtSettings:SecurityKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(bearerOptions =>
                {
                    bearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        //ValidIssuer = config.GetSection("JwtValidIssuer").Get<string>(),
                        //ValidAudience = config.GetSection("JwtValidAudience").Get<string>(),
                        ValidIssuer = config["JwtSettings:ValidIssuer"],
                        ValidAudience = config["JwtSettings:ValidAudience"],
                        IssuerSigningKey = key,
                        ClockSkew = TimeSpan.Zero,
                    };
                    bearerOptions.SaveToken = true;
                    bearerOptions.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[config["JwtSettings:CookieName"]];
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddCookie(cookieOptions =>
                {
                    cookieOptions.Cookie.SameSite = SameSiteMode.Strict;
                    cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    cookieOptions.Cookie.IsEssential = true;
                });

            services.AddScoped<TokenService>();
        }
    }
}
