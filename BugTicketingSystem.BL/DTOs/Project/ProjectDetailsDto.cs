using BugTicketingSystem.BL.DTOs.Bugs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.DTOs.Project
{
    public class ProjectDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BugListDto> Bugs { get; set; } = new();
    }
}
