using AutoMapper;
using FullStackDemo.ApplicationService.DTOs.MobileSuits;
using FullStackDemo.ApplicationService.Queries.Interfaces.IMobileSuits;
using FullStackDemo.ApplicationService.Queries.QueryModels.MobileSuits;
using FullStackDemo.Domain.Entities.MobileSuits;
using FullStackDemo.Domain.RepositoriesInterface.IMobileSuits;
using log4net;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Queries.QueryHandlers.MobileSuits
{
    public class MobileSuitQueryHandler : IMobileSuitQueryHandler
    {
        private readonly static ILog _log = LogManager.GetLogger(typeof(MobileSuitQueryHandler));
        private readonly IMobileSuitQueryRepository _mobileSuitQueryRepository;
        private readonly IMapper _mapper;

        public MobileSuitQueryHandler(IMobileSuitQueryRepository mobileSuitQueryRepository,
                                      IMapper mapper)
        {
            _mobileSuitQueryRepository = mobileSuitQueryRepository;
            _mapper = mapper;
        }

        // Asynchronously handles the GetMobileSuitPaginated query to retrieve paginated Mobile Suit data
        public async Task<MobileSuitPaginatedDto> HandleGetMobileSuitPaginated(GetMobileSuitPaginatedQuery query)
        {
            MobileSuitPaginatedDto result = new MobileSuitPaginatedDto();

            try
            {
                // Call the repository method to get paginated Mobile Suit data
                var res = await _mobileSuitQueryRepository.GetMobileSuitPaginatedAsync(query.search, query.start, query.length);

                // Map the retrieved data to the DTO
                result = _mapper.Map<MobileSuitPaginated, MobileSuitPaginatedDto>(res);
            }
            catch (Exception ex)
            {
                // Log the error message, including inner exception if available
                _log.Error($"HandleGetMobileSuitPaginated: [{ex.InnerException?.Message ?? ex.Message}]");

                throw new ApplicationException(
                   ex.InnerException != null ? "InnerException Error: " : "Exception Error: ",
                   ex.InnerException ?? ex
                );
            }

            // Return the result DTO, which may be empty if an error occurred
            return result;
        }

        public async Task<MobileSuitDto> HandleGetMobileSuitById(GetMobileSuitByIdQuery query) 
        { 
            MobileSuitDto result = new MobileSuitDto();

            try 
            {
                // Call the repository method to retrieve the MobileSuit entity by its ID
                var res = await _mobileSuitQueryRepository.GetMobileSuitByIdAsync(query.id);

                // Map the retrieved MobileSuit entity to the MobileSuitDto
                result = _mapper.Map<MobileSuit, MobileSuitDto>(res);
            }
            catch(Exception ex) {
                // Log the error message, including inner exception if available
                _log.Error($"HandleGetMobileSuitById: [{ex.InnerException?.Message ?? ex.Message}]");

                throw new ApplicationException(
                   ex.InnerException != null ? "InnerException Error: " : "Exception Error: ",
                   ex.InnerException ?? ex
                );
            }

            // Return the mapped MobileSuitDto result
            return result;
        }
    }
}
