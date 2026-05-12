using FullStackDemo.Domain.Entities.Athentication;
using FullStackDemo.Infrastructure.Persistence.Data.Dapper;
using FullStackDemo.Infrastructure.Persistence.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Infrastructure.Persistence.Data.Seeder
{
    public class DbSeeder
    {
        private readonly IGundamDbContext _gundamDbContext;

        public DbSeeder(IGundamDbContext gundamDbContext) { 
            _gundamDbContext = gundamDbContext;
        }

        public async Task SeedDataAsync()
        {
            // Seed ApiBasicAuth if it doesn't already exist
            if (!await _gundamDbContext.UserBasicAuth.AnyAsync())
            {
                await _gundamDbContext.UserBasicAuth.AddAsync(new UserBasicAuth
                {
                    Username = "dev",
                    Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                    CreatedBy = "f",
                    DateCreated = DateTime.UtcNow,
                    Deleted = false
                });
            }

            // Save changes
            await _gundamDbContext.SaveChangesAsync();
        }
    }
}
