using AutoMapper;
using Dapper;
using FullStackDemo.Domain.Entities.MobileSuits;
using FullStackDemo.Domain.RepositoriesInterface.IMobileSuits;
using FullStackDemo.Infrastructure.Persistence.Data.Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Infrastructure.Persistence.Repositories.MobileSuits
{
    public class MobileSuitsQueryRepository : IMobileSuitQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IGundamDb _gundamDb;

        public MobileSuitsQueryRepository(IMapper mapper, 
                                          IGundamDb gundamDb) {
            _mapper = mapper;
            _gundamDb = gundamDb;
        }

        // Asynchronously retrieves a paginated list of Mobile Suits based on search criteria
        public async Task<MobileSuitPaginated> GetMobileSuitPaginatedAsync(string search, int start, int length) 
        {
            MobileSuitPaginated ret = new MobileSuitPaginated();

            // Define the stored procedure name to be called
            string sp = "EXEC dbo.sp_GetMobileSuitsByPage @start, @length, @search";

            // Create a DynamicParameters object to hold the parameters for the stored procedure
            var param = new DynamicParameters();
            param.Add("@start", start);
            param.Add("@length", length);
            param.Add("@search", search);

            try
            {
                // Call the ReadMultipleDataAsync method to execute the stored procedure
                var res = await _gundamDb.ReadMultipleDataAsync<MobileSuit, DynamicParameters>(sp, param);

                ret.TotalCount = res.TotalCount;
                ret.Data = res.Data;
            }
            catch (Exception ex) {
                // Handle exceptions by wrapping them in an ApplicationException for clarity
                throw new ApplicationException(
                ex.InnerException != null ? "InnerException Error: " : "Exception Error: ",
                ex.InnerException ?? ex
                );
            }
           
            return ret;
        }

        public async Task<MobileSuit> GetMobileSuitByIdAsync(int id)
        {
            MobileSuit ret = new MobileSuit();

            // Define the stored procedure name to be called
            string sp = "EXEC dbo.sp_GetMobileSuitById @id";

            // Create a DynamicParameters object to hold the parameters for the stored procedure
            var param = new DynamicParameters();
            param.Add("@id", id);

            try
            {
                // Call the ReadMultipleDataAsync method to execute the stored procedure
                var res = await _gundamDb.ReadDataAsync<MobileSuit, DynamicParameters>(sp, param);

                if (res.Count() > 0) {
                    ret = res.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions by wrapping them in an ApplicationException for clarity
                throw new ApplicationException(
                ex.InnerException != null ? "InnerException Error: " : "Exception Error: ",
                ex.InnerException ?? ex
                );
            }

            return ret;
        }
    }
}
