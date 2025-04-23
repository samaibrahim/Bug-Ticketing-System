using BugTicketingSystem.BL.DTOs.Bugs;
using BugTicketingSystem.BL.Managers.Bugs;
using BugTicketingSystem.BL.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly IBugManager _bugManager;
        public BugController(IBugManager bugManager)
        {
            _bugManager = bugManager;
        }

        [HttpPost]
        [Route("bugs")]
        public async Task<Results<Ok<GeneralResults>, BadRequest<GeneralResults>>> AddBugAsync(BugCreateDto BugDto)
        {
            var result = await _bugManager.AddAsync(BugDto);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }

        }

        [HttpGet]
        [Route("bugs")]
        public async Task<Results<Ok<IEnumerable<BugListDto>>, BadRequest<GeneralResults>>> GetAllBugsAsync()
        {
            var result = await _bugManager.GetAllAsync();
            if (result != null)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(new GeneralResults
                {
                    Success = false,
                    Errors = new[] { new ResultError { Code = "NoBugs", Message = "No bugs found" } }
                });
            }
        }

        [HttpGet]
        [Route("bugs/{id}")]
        public async Task<Results<Ok<BugDetailsDto>, BadRequest<GeneralResults>>> GetBugByIdAsync(Guid id)
        {
            var result = await _bugManager.GetByIdAsync(id);
            if (result != null)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(new GeneralResults
                {
                    Success = false,
                    Errors = new[] { new ResultError { Code = "BugNotFound", Message = "Bug not found" } }
                });
            }
        }

        [HttpPost]
        [Route("bugs/{bugId}/assignees/{userId}")]
        public async Task<Results<Ok<GeneralResults>, BadRequest<GeneralResults>>> AssignUserToBugAsync(Guid bugId, Guid userId)
        {
            var result = await _bugManager.AssignUserToBugAsync(bugId, userId);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("bugs/{bugId}/assignees/{userId}")]
        public async Task<Results<Ok<GeneralResults>, BadRequest<GeneralResults>>> RemoveUserFromBugAsync(Guid bugId, Guid userId)
        {
            var result = await _bugManager.RemoveUserFromBugAsync(bugId, userId);
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
