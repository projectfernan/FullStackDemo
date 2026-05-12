using FullStackDemo.Domain.RepositoriesInterface.IAuthentication;
using FullStackDemo.Domain.RepositoriesInterface.IMobileSuits;
using FullStackDemo.Infrastructure.Persistence.Repositories.Authentication;
using FullStackDemo.Infrastructure.Persistence.Repositories.MobileSuits;

namespace FullStackDemo.WebApi.Configurations
{
    public static class RepositoryConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMobileSuitQueryRepository, MobileSuitsQueryRepository>();
            services.AddScoped<IMobileSuitsCommandRepository, MobileSuitsCommandRepository>();
            services.AddScoped<IAuthenticationQueryRepository, AuthenticationQueryRepository>();
        }
    }
}
