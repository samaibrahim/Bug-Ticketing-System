using BugTicketingSystem.BL.DTOs.Bugs;
using BugTicketingSystem.BL.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Managers.Bugs
{
    public interface IBugManager
    {
        Task<GeneralResults> AddAsync(BugCreateDto dto);
        Task<IEnumerable<BugListDto>> GetAllAsync();
        Task<BugDetailsDto> GetByIdAsync(Guid id);
        Task<GeneralResults> AssignUserToBugAsync(Guid bugId, Guid userId);
        Task<GeneralResults> RemoveUserFromBugAsync(Guid bugId, Guid userId);
    }
}
