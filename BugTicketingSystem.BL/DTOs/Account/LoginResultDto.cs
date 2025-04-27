using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.DTOs.Account
{
    public class LoginResultDto
    {
            public string Message { get; set; }
            public string Token { get; set; }
    }
}
