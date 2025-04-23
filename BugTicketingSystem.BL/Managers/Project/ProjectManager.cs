using BugTicketingSystem.BL.DTOs.Bugs;
using BugTicketingSystem.BL.DTOs.Project;
using BugTicketingSystem.BL.Results;
using BugTicketingSystem.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Managers.Project
{
    public class ProjectManager : IprojectManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResults> AddAsync(ProjectCreateDto ProjectDto)
        {
            try
            {
                if (ProjectDto == null)
                {
                    return new GeneralResults
                    {
                        Success = false,
                        Errors = new[] { new ResultError { Code = "NullData", Message = "Project data is null" } }
                    };

                }
                var NewProject = new BugTicketingSystem.DAL.Models.Project
                {
                    Id = Guid.NewGuid(),
                    Name = ProjectDto.Name,
                    Description = ProjectDto.Description,
                };

                await _unitOfWork.ProjectRepository.AddProjectAsync(NewProject);
               var saveResult= await _unitOfWork.SaveChangesAsync();

                return saveResult > 0
                    ? new GeneralResults { Success = true }
                    : new GeneralResults
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "SaveFailed",
                            Message = "No changes persisted" }]
                    };
            }
            catch (Exception ex)
            {
                return new GeneralResults
                {
                    Success = false,
                    Errors = new[] {
                    new ResultError {
                        Code = "AddFailed",
                        Message = ex.InnerException?.Message ?? ex.Message
                    }
                    }
                };
            }
        }

        public async Task<IEnumerable<ProjectListDto>> GetAllAsync()
        {
            var Projects = await _unitOfWork.ProjectRepository.GetAllProjectsAsync();
            return Projects.Select(p => new ProjectListDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description

            });
        }

        public async Task<ProjectDetailsDto> GetByIdAsync(Guid id)
        {
            var Project = await _unitOfWork.ProjectRepository.GetProjectByIdAsync(id);
            if (Project == null)
            {
                throw new Exception("Project not found");
            }

            return new ProjectDetailsDto
            {
                Id = Project.Id,
                Name = Project.Name,
                Description = Project.Description,
                Bugs = Project.Bugs.Select(b => new BugListDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                }).ToList()
            };
        }
    }
}
