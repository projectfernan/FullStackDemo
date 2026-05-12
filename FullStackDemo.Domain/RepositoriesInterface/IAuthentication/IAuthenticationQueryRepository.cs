using FullStackDemo.Domain.Entities.Athentication;
using FullStackDemo.Domain.Entities.MobileSuits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Domain.RepositoriesInterface.IAuthentication
{
    public interface IAuthenticationQueryRepository
    {
        Task<UserBasicAuth> GetUserBasicAuthByUsername(string username);
    }
}
