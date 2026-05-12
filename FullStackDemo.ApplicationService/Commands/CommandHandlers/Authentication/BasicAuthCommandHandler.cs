using AutoMapper;
using FullStackDemo.ApplicationService.Commands.Interfaces.Authentication;
using FullStackDemo.ApplicationService.Commands.Interfaces.IAuthentication;
using FullStackDemo.ApplicationService.DTOs.Authentication;
using FullStackDemo.ApplicationService.DTOs.MobileSuits;
using FullStackDemo.ApplicationService.Queries.QueryHandlers.MobileSuits;
using FullStackDemo.Domain.Entities.Athentication;
using FullStackDemo.Domain.Entities.MobileSuits;
using FullStackDemo.Domain.RepositoriesInterface.IAuthentication;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FullStackDemo.ApplicationService.Commands.CommandHandlers.Authentication
{
    public class BasicAuthCommandHandler : IBasicAuthCommandHandler
    {
        private readonly static ILog _log = LogManager.GetLogger(typeof(MobileSuitQueryHandler));
        private readonly IMapper _mapper;
        private readonly IAuthenticationQueryRepository _authenticationQueryRepository;

        public BasicAuthCommandHandler(IAuthenticationQueryRepository authenticationQueryRepository,
                                       IMapper mapper) 
        { 
            _authenticationQueryRepository = authenticationQueryRepository;
            _mapper = mapper;
        }

        public async Task<UserBasicAuthDto> HandleGetUserBasicAuthByUsername(BasicAuthCommand data) 
        {
            UserBasicAuthDto result = new UserBasicAuthDto();

            try
            {
                // Call the repository method to retrieve the UserBasicAuth entity by its Username
                var res = await _authenticationQueryRepository.GetUserBasicAuthByUsername(data.Username);

                // Map the retrieved UserBasicAuth entity to the UserBasicAuthDto
                result = _mapper.Map<UserBasicAuth, UserBasicAuthDto>(res);
            }
            catch (Exception ex)
            {
                // Log the error message, including inner exception if available
                _log.Error($"HandleGetMobileSuitById: [{ex.InnerException?.Message ?? ex.Message}]");

                throw new ApplicationException(
                   ex.InnerException != null ? "InnerException Error: " : "Exception Error: ",
                   ex.InnerException ?? ex
                );
            }

            // Return the mapped UserBasicAuthDto result
            return result;
        }
    }
}
