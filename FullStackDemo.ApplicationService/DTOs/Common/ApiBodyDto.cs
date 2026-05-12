using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.DTOs.Common
{
    public class ApiBodyDto
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Request successful.";
        public int TotalCount { get; set; } = 0;
        public object Data { get; set; }
    }
}
