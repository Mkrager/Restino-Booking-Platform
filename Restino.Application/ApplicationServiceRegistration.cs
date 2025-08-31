using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Restino.Application.Contracts.Application;
using Restino.Application.Services;
using System.Reflection;

namespace Restino.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection
            services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IPermissionService, PermissionService>();

            return services;
        }
    }
}
