using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Domain.Entities.MobileSuits
{
    public class MobileSuitPaginated
    {
        public IEnumerable<MobileSuit> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
