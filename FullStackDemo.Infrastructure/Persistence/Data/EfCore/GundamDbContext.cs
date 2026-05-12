using FullStackDemo.Domain.Entities.Athentication;
using FullStackDemo.Domain.Entities.MobileSuits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Infrastructure.Persistence.Data.EfCore
{
    public interface IGundamDbContext
    {
        // DbSet properties for interacting with entities in the database.
        DbSet<MobileSuit> MobileSuits { get; set; }
        DbSet<UserBasicAuth> UserBasicAuth { get; set; }

        EntityEntry Entry(object entity);

        // Asynchronously saves all changes made in this context to the database.
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class GundamDbContext : DbContext, IGundamDbContext
    {
        // Constructor that accepts DbContextOptions to configure the database context.
        public GundamDbContext(DbContextOptions<GundamDbContext> options) : base(options)
        {
        }

        // DbSet for entities, enabling CRUD operations on the corresponding database table
        public DbSet<MobileSuit> MobileSuits { get; set; }
        public DbSet<UserBasicAuth> UserBasicAuth { get; set; }

        public EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        // Implements SaveChangesAsync to handle database save operations with custom exception handling.
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int res = 0;

            try
            {
                res = await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // Custom exception handling, rethrows an ApplicationException with detailed error information.
                throw new ApplicationException(
                  ex.InnerException != null ? "EfCore InnerException Error: " : "EfCore Exception Error: ",
                  ex.InnerException ?? ex
                );
            }

            return res;
        }
    }
}
