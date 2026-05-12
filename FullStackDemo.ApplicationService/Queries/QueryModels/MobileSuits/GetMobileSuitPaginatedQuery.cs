using FullStackDemo.ApplicationService.DTOs.MobileSuits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Queries.QueryModels.MobileSuits
{
    public class GetMobileSuitPaginatedQuery
    {
        public int start { get; set; }
        public int length { get; set; }
        public string? search { get; set; }
    }
}
