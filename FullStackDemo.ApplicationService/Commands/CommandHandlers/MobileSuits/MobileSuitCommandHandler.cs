using AutoMapper;
using FullStackDemo.ApplicationService.Commands.CommandModels.MobileSuits;
using FullStackDemo.ApplicationService.Commands.Interfaces.IMobilesSuits;
using FullStackDemo.ApplicationService.Queries.QueryHandlers.MobileSuits;
using FullStackDemo.Domain.Entities.MobileSuits;
using FullStackDemo.Domain.RepositoriesInterface.IMobileSuits;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Commands.CommandHandlers.MobileSuits
{
    public class MobileSuitCommandHandler : IMobileSuitCommandHandler
    {
        private readonly static ILog _log = LogManager.GetLogger(typeof(MobileSuitQueryHandler));
        private readonly IMapper _mapper;
        private readonly IMobileSuitsCommandRepository _mobileSuitsCommandRepository;

        public MobileSuitCommandHandler(IMapper mapper,
                                        IMobileSuitsCommandRepository mobileSuitsCommandRepository)
        {
            _mapper = mapper;
            _mobileSuitsCommandRepository = mobileSuitsCommandRepository;
        }

        public async Task<int> HandleCreateMobileSuit(CreateMobileSuitCommand data)
        {
            int res = 0;

            // Map the incoming command data to the MobileSuit entity
            var mobileSuit = _mapper.Map<CreateMobileSuitCommand, MobileSuit>(data);

            try
            {
                // Call the repository method to create a new MobileSuit entity in the database
                res = await _mobileSuitsCommandRepository.CreateMobileSuitAsync(mobileSuit);
            }
            catch (Exception ex)
            {
                // Log the error message, including inner exception if available
                _log.Error($"HandleCreateMobileSuit: [{ex.InnerException?.Message ?? ex.Message}]");

                throw new ApplicationException(
                   ex.InnerException != null ? "InnerException Error: " : "Exception Error: ",
                   ex.InnerException ?? ex
                );
            }

            return res;
        }

        public async Task<int> HandleUpdateMobileSuit(UpdateMobileSuitCommand data)
        {
            int res = 0;

            // Map the incoming command data to the MobileSuit entity
            var mobileSuit = _mapper.Map<UpdateMobileSuitCommand, MobileSuit>(data);

            try
            {
                // Call the repository method to update an existing MobileSuit entity in the database
                res = await _mobileSuitsCommandRepository.UpdateMobileSuitAsync(mobileSuit);
            }
            catch (Exception ex)
            {
                // Log the error message, including inner exception if available
                _log.Error($"HandleUpdateMobileSuit: [{ex.InnerException?.Message ?? ex.Message}]");

                throw new ApplicationException(
                   ex.InnerException != null ? "InnerException Error: " : "Exception Error: ",
                   ex.InnerException ?? ex
                );
            }

            return res;
        }

        public async Task<int> HandleDeleteMobileSuit(DeleteMobileSuitCommand data)
        {
            int res = 0;

            // Map the incoming command data to the MobileSuit entity
            var mobileSuit = _mapper.Map<DeleteMobileSuitCommand, MobileSuit>(data);

            try
            {
                // Call the repository method to delete the MobileSuit entity from the database
                res = await _mobileSuitsCommandRepository.DeleteMobileSuitAsync(mobileSuit);
            }
            catch (Exception ex)
            {
                // Log the error message, including inner exception if available
                _log.Error($"HandleDeleteMobileSuit: [{ex.InnerException?.Message ?? ex.Message}]");

                throw new ApplicationException(
                   ex.InnerException != null ? "InnerException Error: " : "Exception Error: ",
                   ex.InnerException ?? ex
                );
            }

            return res;
        }
    }
}
