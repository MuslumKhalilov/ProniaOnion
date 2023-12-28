using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Infrastructure.Implementations;

namespace ProniaOnion.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    LifetimeValidator = (notBefore,expired,token,param) => token != null?expired> DateTime.UtcNow:false
                };
            });
            services.AddAuthorization();
            services.AddScoped<ITokenHandler, TokenHandler>();
            return services;
        }
    }
}
