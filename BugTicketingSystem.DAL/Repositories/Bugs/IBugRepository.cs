using BugTicketingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Repositories.Bugs
{
    public interface IBugRepository
    {
        Task<IEnumerable<Bug>> GetAllBugsAsync();
        Task<Bug> GetBugByIdAsync(Guid id);
        Task AddBugAsync(Bug bug);

        //Assign and Unassign user methods
        Task<bool> AssignUserToBugAsync(Guid bugId, Guid userId);
        Task<bool> RemoveUserFromBugAsync(Guid bugId, Guid userId);

    }
}
