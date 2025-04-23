using BugTicketingSystem.DAL.Context;
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
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BugTicketingSystemContext _context;

        public IProjectRepository ProjectRepository { get; }
        public IBugRepository BugRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }

        public UnitOfWork(BugTicketingSystemContext context,
                          IProjectRepository projectRepository,
                          IBugRepository bugRepository,
                          IAttachmentRepository attachmentRepository)
        {
            _context = context;
            ProjectRepository = projectRepository;
            BugRepository = bugRepository;
            AttachmentRepository = attachmentRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
