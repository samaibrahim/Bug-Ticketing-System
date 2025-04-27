using BugTicketingSystem.BL.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Managers.Account
{
    public interface IAccountManager
    {
        Task<string> Register(RegisterDto register);
        Task<LoginResultDto> Login(LoginDto login);
       
    }
}
