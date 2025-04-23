using BugTicketingSystem.BL.DTOs.Project;
using BugTicketingSystem.BL.Managers.Project;
using BugTicketingSystem.BL.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IprojectManager _projectManager;
        public ProjectController(IprojectManager projectManager)
        {
            _projectManager = projectManager;
        }
        [HttpGet]
        [Route("projects")]
        public async Task<Results<Ok<IEnumerable<ProjectListDto>>, BadRequest<GeneralResults>>> GetAllProjectsAsync()
        {
            var result = await _projectManager.GetAllAsync();
            if (result != null)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(new GeneralResults
                {
                    Success = false,
                    Errors = new[] { new ResultError { Code = "NoProjects", Message = "No projects found" } }
                });
            }
        }

        [HttpGet]
        [Route("projects/{id}")]
        public async Task<Results<Ok<ProjectDetailsDto>, BadRequest<GeneralResults>>> GetProjectByIdAsync(Guid id)
        {
            var result = await _projectManager.GetByIdAsync(id);
            if (result != null)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(new GeneralResults
                {
                    Success = false,
                    Errors = new[] { new ResultError { Code = "NoProject", Message = "No project found" } }
                });
            }
        }

        [HttpPost]
        [Route("projects")]
        public async Task<Results<Ok<GeneralResults>, BadRequest<GeneralResults>>> AddProjectAsync(ProjectCreateDto projectDto)
        {
            var result = await _projectManager.AddAsync(projectDto);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }
        }
    }
}
