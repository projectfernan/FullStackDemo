using FullStackDemo.Domain.Entities.MobileSuits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Domain.RepositoriesInterface.IMobileSuits
{
    public interface IMobileSuitsCommandRepository
    {
        Task<int> CreateMobileSuitAsync(MobileSuit data);
        Task<int> UpdateMobileSuitAsync(MobileSuit data);
        Task<int> DeleteMobileSuitAsync(MobileSuit data);
    }
}
