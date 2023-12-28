using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Infrastructure.Implementations;

namespace ProniaOnion.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler,TokenHandler>();
            return services;
        }
    }
}
