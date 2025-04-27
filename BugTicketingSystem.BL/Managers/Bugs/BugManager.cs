using BugTicketingSystem.BL.DTOs.Attachement;
using BugTicketingSystem.BL.DTOs.Bugs;
using BugTicketingSystem.BL.DTOs.User;
using BugTicketingSystem.BL.Results;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Managers.Bugs
{
    public class BugManager : IBugManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public BugManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork; 
        }
        public async Task<GeneralResults> AddAsync(BugCreateDto BugDto)
        {
            try
            {
                if (BugDto == null)
                {
                    return new GeneralResults
                    {
                        Success = false,
                        Errors = new[] { new ResultError { Code = "NullData", Message = "Bug data is null" } }
                    };
                }
                var NewBug = new Bug
                {
                    Id = Guid.NewGuid(),
                    Title = BugDto.Title,
                    Description = BugDto.Description,
                    ProjectId = BugDto.ProjectId
                };
                await _unitOfWork.BugRepository.AddBugAsync(NewBug);
                var saveResult = await _unitOfWork.SaveChangesAsync();

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

        public async Task<IEnumerable<BugListDto>> GetAllAsync()
        {
            var Bugs= await _unitOfWork.BugRepository.GetAllBugsAsync();
            return Bugs.Select(b => new BugListDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ProjectId = b.ProjectId

            });
        }

        public async Task<BugDetailsDto> GetByIdAsync(Guid id)
        {
            var bug = await _unitOfWork.BugRepository.GetBugByIdAsync(id);
            if (bug == null)
            {
                throw new Exception("Bug not found");
            }
            return new BugDetailsDto
            {
                Id = bug.Id,
                Title = bug.Title,
                Description = bug.Description,
                ProjectId = bug.ProjectId,
                Assignees = bug.Assigns.Select(a => new UserDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Email = a.Email,

                }).ToList(),
                Attachments = bug.Attachements.Select(a => new AttachementDto
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    FilePath = a.FilePath
                }).ToList()

            };

        }

        public async Task<GeneralResults> AssignUserToBugAsync(Guid bugId, Guid userId)
        {
            var Success = await _unitOfWork.BugRepository.AssignUserToBugAsync(bugId, userId);
            if (!Success)
            {
                return new GeneralResults
                {
                    Success = false,
                    Errors = new[]
                    {
                        new ResultError
                        {
                            Code = "AssignFailed",
                            Message = "Could not assign user to bug"
                        }
                    }
                };
            }
            await _unitOfWork.SaveChangesAsync();
            return new GeneralResults
            {
                Success = true
            };

        }
        public async Task<GeneralResults> RemoveUserFromBugAsync(Guid bugId, Guid userId)
        {
            var Success = await _unitOfWork.BugRepository.RemoveUserFromBugAsync(bugId, userId);
            if (!Success)
            {
                return new GeneralResults
                {
                    Success = false,
                    Errors = new[]
                    {
                        new ResultError
                        {
                            Code = "RemoveUserFromBugFailed",
                            Message = "Could not Remove user from bug"
                        }
                    }
                };
            }
            await _unitOfWork.SaveChangesAsync();
            return new GeneralResults
            {
                Success = true
            };
        }
    }
}
