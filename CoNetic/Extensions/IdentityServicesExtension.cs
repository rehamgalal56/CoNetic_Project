using CoNetic.Core.Models;
using CoNetic.Core.ServicesInterfaces;
using CoNetic.Repository.Identity;
using CoNetic.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CoNetic.Core.ReposInterfaces;
using CoNetic.Repository.Repos;
namespace CoNetic.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration Configuration)
        {
            
            services.AddScoped<ITokenService, TokenService>();
            services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
            services.AddAuthentication();
            services.AddAuthentication(options =>
            {
                options. DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),


                    };
                }).AddGoogle(options =>
                {
                    options.ClientId = Configuration["Google:ClientId"];
                    options.ClientSecret = Configuration["Google:ClientSecret"];
                });


            return services;
        }
    }
}