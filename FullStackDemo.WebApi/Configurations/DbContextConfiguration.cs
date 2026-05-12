using FullStackDemo.Infrastructure.Persistence.Data.EfCore;
using FullStackDemo.Infrastructure.Persistence.Data.Seeder;
using Microsoft.EntityFrameworkCore;

namespace FullStackDemo.WebApi.Configurations
{
    public static class DbContextConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IGundamDbContext, GundamDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GundamDb"),
                    b => b.MigrationsAssembly("FullStackDemo.Infrastructure")
            ));
        }

        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GundamDbContext>();
                var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();

                // Seed data
                await seeder.SeedDataAsync();
            }
        }
    }
}
