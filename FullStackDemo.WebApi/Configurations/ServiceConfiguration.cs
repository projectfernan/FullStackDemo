using FullStackDemo.ApplicationService.Commands.CommandHandlers.Authentication;
using FullStackDemo.ApplicationService.Commands.CommandHandlers.MobileSuits;
using FullStackDemo.ApplicationService.Commands.Interfaces.IAuthentication;
using FullStackDemo.ApplicationService.Commands.Interfaces.IMobilesSuits;
using FullStackDemo.ApplicationService.Queries.Interfaces.IAuthentication;
using FullStackDemo.ApplicationService.Queries.Interfaces.IMobileSuits;
using FullStackDemo.ApplicationService.Queries.QueryHandlers.Authentication;
using FullStackDemo.ApplicationService.Queries.QueryHandlers.MobileSuits;
using FullStackDemo.Infrastructure.Persistence.Data.Seeder;

namespace FullStackDemo.WebApi.Configurations
{
    public static class ServiceConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMobileSuitQueryHandler, MobileSuitQueryHandler>();
            services.AddScoped<IMobileSuitCommandHandler, MobileSuitCommandHandler>();
            services.AddScoped<IBasicAuthCommandHandler, BasicAuthCommandHandler>();
            services.AddScoped<IGetJwtTokenHandler, GetJwtTokenHandler>();

            services.AddTransient<DbSeeder>();
        }
    }
}
