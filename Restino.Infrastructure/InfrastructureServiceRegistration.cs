using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restino.Application.Contracts.Infrastructure;
using Restino.Application.DTOs.Mail;
using Restino.Infrastructure.Mail;

namespace Restino.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
