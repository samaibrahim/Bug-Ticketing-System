using BugTicketingSystem.BL.Managers.Account;
using BugTicketingSystem.BL.Managers.Attachement;
using BugTicketingSystem.BL.Managers.Bugs;
using BugTicketingSystem.BL.Managers.Project;
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
    public static class BusinessExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            

            services.AddScoped<IBugManager, BugManager>();
            services.AddScoped<IprojectManager, ProjectManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IAttachementManager, AttachementManager>();
        }
    }
}
