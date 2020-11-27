using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http;
using Ctron.Infrastructure;
using Ctron.API.Authentication;
using Ctron.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Ctron.API.Configuration
{
    public class ModuleConfiguration

    {
        private IServiceCollection _services;
        private IConfiguration _config;

        public ModuleConfiguration(IServiceCollection services, IConfiguration config)
        {
            _services = services;
            _config = config;
        }

        public void ConfigureServices()
        {
            #region Configure Services
            _services.AddScoped<IJwtAuthentication, JwtAuthentication>();
            _services.AddScoped<IApiAuthentication, ApiAuthentication>();
            #endregion

            #region Configure Variables
            _services.Configure<JWTConfig>(_config.GetSection("Jwt"));
            #endregion
        }

        public void ConfigureDatabase()
        {
            _services.AddDbContext(_config.GetConnectionString("DefaultConnection"));
        }

        public void ConfigureSecurity()
        {
            //Identity
            _services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            _services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            });

            //JWT
            _services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _config["Jwt:Issuer"],
                        ValidAudience = _config["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
                    };
                });
        }
    }
}
