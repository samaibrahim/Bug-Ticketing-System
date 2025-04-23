using BugTicketingSystem.BL.DTOs.Attachement;
using BugTicketingSystem.BL.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.DTOs.Bugs
{
    public class BugDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }

        public List<UserDto> Assignees { get; set; } = new();
        public List<AttachementDto> Attachments { get; set; } = new();
    }
}
