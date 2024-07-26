using LogginLibrary.LogginRequest;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LogginLibrary
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddLogginService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviours<,>));
            return services;
        }
    }
}
