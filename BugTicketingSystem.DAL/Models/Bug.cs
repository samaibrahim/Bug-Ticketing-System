using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Models
{
    public class Bug
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        //relationship
        public ICollection<Users> Assigns { get; set; } = new HashSet<Users>();
        public ICollection<Attachement> Attachements { get; set; } = new HashSet<Attachement>();
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
