using BugTicketingSystem.BL.DTOs.Attachement;
using BugTicketingSystem.BL.DTOs.Bugs;
using BugTicketingSystem.BL.Managers.Attachement;
using BugTicketingSystem.BL.Managers.Bugs;
using BugTicketingSystem.BL.Results;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAttachementManager _attachementManager;
        public BugController(IBugManager bugManager, IAttachementManager attachementManager)
        {
            _bugManager = bugManager;
            _attachementManager = attachementManager;
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
        [Authorize(Roles = "Manager")]
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
        // POST: api/bugs/{id}/attachments
        [HttpPost("bugs/{bugId}/attachments")]
        
        public async Task<IActionResult> UploadAttachment(Guid bugId, [FromForm] UploadAttachementDto dto)
        {
            if (dto == null || dto.File == null || dto.File.Length == 0)
            {
                return BadRequest(new GeneralResults
                {
                    Success = false,
                    Errors = new[] { new ResultError { Code = "FileEmpty", Message = "No file provided." } }
                });
            }

            var result = await _attachementManager.UploadAttachmentAsync(bugId, dto);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            result.Data.FilePath = $"{baseUrl}/{result.Data.FilePath}";
            return Ok(result);  // Return the result with the attachment data
        }

        // GET: api/bugs/{id}/attachments
        [HttpGet("bugs/{bugId}/attachments")]
        public async Task<IActionResult> GetAttachmentsForBug(Guid bugId)
        {
            var result = await _attachementManager.GetAttachmentsForBugAsync(bugId);

            if (!result.Success)
            {
                return NotFound(result);  // If there are no attachments, return NotFound
            }

            return Ok(result);  // Return the list of attachments
        }

        // DELETE: api/bugs/{id}/attachments/{attachmentId}
        [HttpDelete("bugs/{bugId}/attachments/{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(Guid bugId, Guid attachmentId)
        {
            var result = await _attachementManager.DeleteAttachmentAsync(bugId, attachmentId);

            if (!result.Success)
            {
                return NotFound(result);  // If the attachment is not found, return NotFound
            }

            return NoContent();  // Return 204 No Content if deletion is successful
        }

    }
}
