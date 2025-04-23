using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Repositories.Attachements
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly BugTicketingSystemContext _Context;

        public AttachmentRepository(BugTicketingSystemContext context)
        {
            _Context = context;
        }

        // Upload Attachment
        public async Task UploadAttachmentAsync(Guid bugId, Attachement attachment)
        {
            var bug = await _Context.Set<Bug>()
                .Include(b => b.Attachements)
                .FirstOrDefaultAsync(b => b.Id == bugId);

            if (bug == null)
                throw new Exception("Bug not found");

            bug.Attachements.Add(attachment);
            
        }

        // Get All Attachments for a Bug
        public async Task<List<Attachement>> GetAttachmentsForBugAsync(Guid bugId)
        {
            var bug = await _Context.Set<Bug>()
                .Include(b => b.Attachements)
                .FirstOrDefaultAsync(b => b.Id == bugId);

            if (bug == null)
                throw new Exception("Bug not found");

            return bug.Attachements.ToList();
        }

        // Delete Attachment
        public async Task DeleteAttachmentAsync(Guid bugId, Guid attachmentId)
        {
            var bug = await _Context.Set<Bug>()
                .Include(b => b.Attachements)
                .FirstOrDefaultAsync(b => b.Id == bugId);

            if (bug == null)
                throw new Exception("Bug not found");

            var attachment = bug.Attachements.FirstOrDefault(a => a.Id == attachmentId);
            if (attachment == null)
                throw new Exception("Attachment not found");

            bug.Attachements.Remove(attachment);
            
        }
    }
}

