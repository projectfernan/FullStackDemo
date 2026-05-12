using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.DTOs.MobileSuits
{
    public class MobileSuitPaginatedDto
    {
        public IEnumerable<MobileSuitDto> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
