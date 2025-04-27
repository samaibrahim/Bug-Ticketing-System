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

        public async Task<bool> DeleteAttachmentAsync(Guid bugId, Guid attachmentId)
        {
            var attachment = await _Context.Attachements
            .FirstOrDefaultAsync(a => a.Id == attachmentId && a.BugId == bugId);

            if (attachment == null)
            {
                return false;
            }

            _Context.Attachements.Remove(attachment);
            //await _Context.SaveChangesAsync();

            return true;
        
        }

        public async Task<IEnumerable<Attachement>> GetAttachmentsByBugIdAsync(Guid bugId)
        {
            return await _Context.Attachements
            .Where(a => a.BugId == bugId)
            .ToListAsync();
        }

        public async Task<Attachement> UploadAttachmentAsync(Guid bugId, Attachement attachment)
        {
            
            var bug = await _Context.Bugs.FirstOrDefaultAsync(b => b.Id == bugId);
            if (bug == null)
            {
                return null;
            }
            attachment.BugId = bugId;

            await _Context.Attachements.AddAsync(attachment);
           // await _Context.SaveChangesAsync();

            return attachment;
        }
    }
}

