using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace BugTicketingSystem.BL.DTOs.Attachement
{
    public class UploadAttachementDto
    {
            public string FileName { get; set; }
            public IFormFile File { get; set; }
    }
}
