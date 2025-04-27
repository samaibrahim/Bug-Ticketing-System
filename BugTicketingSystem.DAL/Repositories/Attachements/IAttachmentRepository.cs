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
        // Upload attachment to bug
        Task<Attachement> UploadAttachmentAsync(Guid bugId, Attachement attachment);
        // Get attachments for bug
        Task<IEnumerable<Attachement>> GetAttachmentsByBugIdAsync(Guid bugId);
        // Delete attachment from bug
        Task<bool> DeleteAttachmentAsync(Guid bugId, Guid attachmentId); 

    }
}
