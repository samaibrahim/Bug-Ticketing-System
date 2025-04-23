using BugTicketingSystem.DAL.Repositories.Attachements;
using BugTicketingSystem.DAL.Repositories.Bugs;
using BugTicketingSystem.DAL.Repositories.projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IBugRepository BugRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
