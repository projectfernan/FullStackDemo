using FullStackDemo.ApplicationService.Commands.Interfaces.Authentication;
using FullStackDemo.ApplicationService.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Commands.Interfaces.IAuthentication
{
    public interface IBasicAuthCommandHandler
    {
        Task<UserBasicAuthDto> HandleGetUserBasicAuthByUsername(BasicAuthCommand data);
    }
}
