using FullStackDemo.Domain.Entities.MobileSuits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Domain.RepositoriesInterface.IMobileSuits
{
    public interface IMobileSuitQueryRepository
    {
        Task<MobileSuitPaginated> GetMobileSuitPaginatedAsync(string search, int start, int length);
        Task<MobileSuit> GetMobileSuitByIdAsync(int id);
    }
}
