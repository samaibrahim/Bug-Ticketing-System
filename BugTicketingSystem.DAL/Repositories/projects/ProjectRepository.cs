using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Repositories.projects
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly BugTicketingSystemContext _Context;
        public ProjectRepository(BugTicketingSystemContext Context)
        {
            _Context= Context;
        }
        public async Task AddProjectAsync(Project project)
        {
            await _Context.Set<Project>().AddAsync(project);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _Context.Set<Project>()
                    .Include(p => p.Bugs)
                    .ThenInclude(b => b.Attachements)
                    .Include(p => p.Bugs)
                    .ThenInclude(b => b.Assigns)
                    .AsSplitQuery()
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(Guid id)
        {
            return await _Context.Set<Project>()
                    .Include(p => p.Bugs)
                    .ThenInclude(b => b.Attachements)
                    .Include(p => p.Bugs)
                    .ThenInclude(b => b.Assigns)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(p => p.Id == id);      
        }
    }
}
