using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.EntitiesConfigurations
{
    public class UserConfiguration:IEntityTypeConfiguration<Users>
    {

        
            public void Configure(EntityTypeBuilder<Users> builder)
            {
                
                builder.ToTable("Users");
                builder.Property(u => u.FullName)
                  .HasMaxLength(255);

            }
        

    }
}
