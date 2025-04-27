using BugTicketingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Repositories.Attachements;
using BugTicketingSystem.BL.DTOs.Attachement;
using BugTicketingSystem.BL.Results;

namespace BugTicketingSystem.BL.Managers.Attachement
{
    public interface IAttachementManager
    {
        Task<GeneralResult<AttachementDto>> UploadAttachmentAsync(Guid bugId, UploadAttachementDto dto);
        Task<GeneralResult<List<AttachementDto>>> GetAttachmentsForBugAsync(Guid bugId);
        Task<GeneralResults> DeleteAttachmentAsync(Guid bugId, Guid attachmentId);
    }
}
