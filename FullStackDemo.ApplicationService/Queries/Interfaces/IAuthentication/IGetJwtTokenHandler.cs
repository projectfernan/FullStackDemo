using FullStackDemo.ApplicationService.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Queries.Interfaces.IAuthentication
{
    public interface IGetJwtTokenHandler
    {
        JwtTokenDto GenerateJwtToken();
    }
}
