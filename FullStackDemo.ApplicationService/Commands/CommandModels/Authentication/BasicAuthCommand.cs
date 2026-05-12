using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Commands.Interfaces.Authentication
{
    public class BasicAuthCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
