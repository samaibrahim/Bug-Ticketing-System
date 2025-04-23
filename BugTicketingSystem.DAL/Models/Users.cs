using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Models
{ 
    public class Users :IdentityUser<Guid>
    {
        public string FullName { get; set; }
       
        public ICollection<Bug> AssignBug { get; set; }= new HashSet<Bug>();
    }
}
