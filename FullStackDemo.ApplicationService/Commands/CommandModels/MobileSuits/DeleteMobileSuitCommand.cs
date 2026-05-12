using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Commands.CommandModels.MobileSuits
{
    public class DeleteMobileSuitCommand
    {
        public int Id { get; set; }
        public string DeletedBy { get; set; }
    }
}
