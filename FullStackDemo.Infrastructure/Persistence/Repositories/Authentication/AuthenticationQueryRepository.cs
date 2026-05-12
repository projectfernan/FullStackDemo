using Dapper;
using FullStackDemo.Domain.Entities.Athentication;
using FullStackDemo.Domain.Entities.MobileSuits;
using FullStackDemo.Domain.RepositoriesInterface.IAuthentication;
using FullStackDemo.Infrastructure.Persistence.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Infrastructure.Persistence.Repositories.Authentication
{
    public class AuthenticationQueryRepository : IAuthenticationQueryRepository
    {
        private readonly IGundamDb _gundamDb;
        public AuthenticationQueryRepository(IGundamDb gundamDb) { 
            _gundamDb = gundamDb;
        }

        public async Task<UserBasicAuth> GetUserBasicAuthByUsername(string username) 
        { 
            UserBasicAuth ret = new UserBasicAuth();

            string sp = "EXEC dbo.sp_GetUserBasicAuthByUsername @username";

            // Create a DynamicParameters object to hold the parameters for the stored procedure
            var param = new DynamicParameters();
            param.Add("@username", username);

            try
            {
                // Call the ReadMultipleDataAsync method to execute the stored procedure
                var res = await _gundamDb.ReadDataAsync<UserBasicAuth, DynamicParameters>(sp, param);

                if (res.Count() > 0)
                {
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
