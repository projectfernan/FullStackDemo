using FullStackDemo.ApplicationService.Commands.CommandModels.MobileSuits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Commands.Interfaces.IMobilesSuits
{
    public interface IMobileSuitCommandHandler
    {
        Task<int> HandleCreateMobileSuit(CreateMobileSuitCommand data);
        Task<int> HandleUpdateMobileSuit(UpdateMobileSuitCommand data);
        Task<int> HandleDeleteMobileSuit(DeleteMobileSuitCommand data);
    }
}
