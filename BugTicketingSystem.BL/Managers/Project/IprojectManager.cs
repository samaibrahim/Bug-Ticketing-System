using BugTicketingSystem.BL.DTOs.Project;
using BugTicketingSystem.BL.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Managers.Project
{
    public interface IprojectManager
    {
        Task<IEnumerable<ProjectListDto>> GetAllAsync();
        Task<ProjectDetailsDto> GetByIdAsync(Guid id);
        Task<GeneralResults> AddAsync(ProjectCreateDto dto);
    }
}
