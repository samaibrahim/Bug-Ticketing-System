using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Models
{
    public class Attachement
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        //relationship
        public Guid BugId { get; set; }
        public Bug Bug { get; set; }
    }
}
