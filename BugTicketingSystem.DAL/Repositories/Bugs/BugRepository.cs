using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Repositories.Bugs
{
    public class BugRepository : IBugRepository
    {
        private readonly BugTicketingSystemContext _Context;
        public BugRepository(BugTicketingSystemContext Context)
        {
            _Context = Context;
        }
        public async Task AddBugAsync(Bug bug)
        {
             await _Context.Set<Bug>().AddAsync(bug);
        }


        public async Task<IEnumerable<Bug>> GetAllBugsAsync()
        {
            return await _Context.Set<Bug>()
                .Include(B => B.Project)
                .Include(B =>B.Assigns)
                .Include(B => B.Attachements)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Bug> GetBugByIdAsync(Guid id)
        {
            return await _Context.Set<Bug>()
                .Include(B => B.Project)
                .Include(B => B.Assigns)
                .Include(B => B.Attachements)
                .AsSplitQuery()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> AssignUserToBugAsync(Guid bugId, Guid userId)
        {
            var bug = await _Context.Set<Bug>()
                .Include(b => b.Assigns)
                .FirstOrDefaultAsync(b => b.Id == bugId);

            if (bug == null)
            {
                return false;
            }

            var user = await _Context.Set<Users>().FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            if (!bug.Assigns.Contains(user))
            {
                bug.Assigns.Add(user);
                return true;
            }

            return false; // User already assigned
        }


        public async Task<bool> RemoveUserFromBugAsync(Guid bugId, Guid userId)
        {
            var bug = await _Context.Set<Bug>()
                .Include(b => b.Assigns)
                .FirstOrDefaultAsync(b => b.Id == bugId);

            if (bug == null) return false;

            var user = await _Context.Set<Users>().FindAsync(userId);
            if (user == null) return false;

            if (bug.Assigns.Contains(user))
            {
                bug.Assigns.Remove(user);
                return true;
            }

            return false; // User already not assigned
        }

    }
}
