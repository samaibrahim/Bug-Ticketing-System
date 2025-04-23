using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Repositories.Attachements;
using BugTicketingSystem.DAL.Repositories.Bugs;
using BugTicketingSystem.DAL.Repositories.projects;
using BugTicketingSystem.DAL.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL
{
    public static class DataAccessExtension
    {
        public static void AddDataAccessServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BugTicketingSystemContext>(Options =>
            Options.UseSqlServer(connectionString));

            services.AddScoped<IProjectRepository,ProjectRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IBugRepository, BugRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
