using FullStackDemo.ApplicationService.DTOs.MobileSuits;
using FullStackDemo.ApplicationService.Queries.QueryModels.MobileSuits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Queries.Interfaces.IMobileSuits
{
    public interface IMobileSuitQueryHandler
    {
        Task<MobileSuitPaginatedDto> HandleGetMobileSuitPaginated(GetMobileSuitPaginatedQuery query);
        Task<MobileSuitDto> HandleGetMobileSuitById(GetMobileSuitByIdQuery query);
    }
}
