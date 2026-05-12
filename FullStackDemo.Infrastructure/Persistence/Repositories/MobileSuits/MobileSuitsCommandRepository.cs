using FullStackDemo.Domain.Entities.MobileSuits;
using FullStackDemo.Domain.RepositoriesInterface.IMobileSuits;
using FullStackDemo.Infrastructure.Persistence.Data.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Infrastructure.Persistence.Repositories.MobileSuits
{
    public class MobileSuitsCommandRepository : IMobileSuitsCommandRepository
    {
        private readonly IGundamDbContext _gundamDbContext;
        public MobileSuitsCommandRepository(
                IGundamDbContext gundamDbContext
            ) { 
            _gundamDbContext = gundamDbContext;
        }

        public async Task<int> CreateMobileSuitAsync(MobileSuit data) 
        {
            int res = 0;
            data.DateCreated = DateTime.UtcNow;

            // Asynchronously adds the new MobileSuit entity to the DbSet
            await _gundamDbContext.MobileSuits.AddAsync(data);

            try {
                // Save changes asynchronously to the database
                res = await _gundamDbContext.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                // Throw a more informative ApplicationException with inner exception details
                throw new ApplicationException(
                  ex.InnerException != null ? "EfCore InnerException Error: " : "EfCore Exception Error: ",
                  ex.InnerException ?? ex
                );
            }

            return res; // Return the result (number of affected rows)
        }

        public async Task<int> UpdateMobileSuitAsync(MobileSuit data)
        {
            int res = 0;
            data.DateCreated = data.DateCreated.ToUniversalTime();
            data.DateEdited = DateTime.UtcNow;

            // Update the new MobileSuit entity to the DbSet
            _gundamDbContext.MobileSuits.Update(data);

            try
            {
                // Save changes asynchronously to the database
                res = await _gundamDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Throw a more informative ApplicationException with inner exception details
                throw new ApplicationException(
                  ex.InnerException != null ? "EfCore InnerException Error: " : "EfCore Exception Error: ",
                  ex.InnerException ?? ex
                );
            }

            return res; // Return the result (number of affected rows)
        }

        public async Task<int> DeleteMobileSuitAsync(MobileSuit data)
        {
            int res = 0;

            // Set the properties for soft deletion
            data.Deleted = true;
            data.DateCreated = data.DateCreated.ToUniversalTime();

            // Only convert DateEdited if it has a value
            if (data.DateEdited.HasValue)
                data.DateEdited = data.DateEdited.Value.ToUniversalTime();

            data.DateDeleted = DateTime.UtcNow;

            // Attach the entity to the context and specify which properties to update
            _gundamDbContext.Entry(data).Property("Deleted").IsModified = true;
            _gundamDbContext.Entry(data).Property("DateDeleted").IsModified = true;
            _gundamDbContext.Entry(data).Property("DeletedBy").IsModified = true;

            try
            {
                // Save changes asynchronously to the database
                res = await _gundamDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Throw a more informative ApplicationException with inner exception details
                throw new ApplicationException(
                  ex.InnerException != null ? "EfCore InnerException Error: " : "EfCore Exception Error: ",
                  ex.InnerException ?? ex
                );
            }

            return res; // Return the result (number of affected rows)
        }
    }
}
