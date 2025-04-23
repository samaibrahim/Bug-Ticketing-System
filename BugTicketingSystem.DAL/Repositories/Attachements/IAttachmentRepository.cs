using BugTicketingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Repositories.Attachements
{
    public interface IAttachmentRepository
    {
        Task UploadAttachmentAsync(Guid bugId, Attachement attachment);
        Task<List<Attachement>> GetAttachmentsForBugAsync(Guid bugId);
        Task DeleteAttachmentAsync(Guid bugId, Guid attachmentId);
    }
}
