using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Context
{
    public class BugTicketingSystemContext : IdentityDbContext<Users, IdentityRole<Guid>,Guid>
    {
        public BugTicketingSystemContext(DbContextOptions<BugTicketingSystemContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(BugTicketingSystemContext).Assembly
             );
        }

        public DbSet<Attachement> Attachements { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
