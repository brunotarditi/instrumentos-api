using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using instrumentos_api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace instrumentos_api.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IInstrumentoService, InstrumentoService>();
            return services;
        }
    }
}
