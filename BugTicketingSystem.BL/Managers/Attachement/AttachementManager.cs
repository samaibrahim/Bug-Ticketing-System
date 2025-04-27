using BugTicketingSystem.BL.DTOs.Attachement;
using BugTicketingSystem.BL.Results;
using BugTicketingSystem.DAL.Repositories.Attachements;
using BugTicketingSystem.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Managers.Attachement
{
    public class AttachementManager : IAttachementManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttachementManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResults> DeleteAttachmentAsync(Guid bugId, Guid attachmentId)
        {
            var deleted = await _unitOfWork.AttachmentRepository.DeleteAttachmentAsync(bugId, attachmentId);

            if (!deleted)
            {
                return new GeneralResults
                {
                    Success = false,
                    Errors = new[] { new ResultError { Code = "AttachmentNotFound", Message = "Attachment not found for this bug." } }
                };
            }
            await _unitOfWork.SaveChangesAsync();
            return new GeneralResults { Success = true };
        
        }

        public async Task<GeneralResult<List<AttachementDto>>> GetAttachmentsForBugAsync(Guid bugId)
        {
            var attachments = await _unitOfWork.AttachmentRepository.GetAttachmentsByBugIdAsync(bugId);

            var result = attachments.Select(a => new AttachementDto
            {
                Id = a.Id,
                FileName = a.FileName,
                FilePath = a.FilePath
            }).ToList();

            return new GeneralResult<List<AttachementDto>>
            {
                Success = true,
                Data = result
            };
        }

        public async Task<GeneralResult<AttachementDto>> UploadAttachmentAsync(Guid bugId, UploadAttachementDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
            {
                return new GeneralResult<AttachementDto>
                {
                    Success = false,
                    Errors = new[] { new ResultError { Code = "FileEmpty", Message = "No file provided." } }
                };
            }

            //  saving 
            
            var uniqueFileName = $"{Guid.NewGuid()}_{dto.File.FileName}";

            
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePathOnDisk = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePathOnDisk, FileMode.Create))
            {
                await dto.File.CopyToAsync(fileStream);
            }

            var filePath = $"uploads/{uniqueFileName}";
            var fileUrl = $"/{filePath.Replace("\\", "/")}";


            var attachment = new DAL.Models.Attachement
            {
                Id = Guid.NewGuid(),
                FileName = dto.File.FileName,
                FilePath = fileUrl, 
                BugId = bugId
            };

            var addedAttachment = await _unitOfWork.AttachmentRepository.UploadAttachmentAsync(bugId, attachment);

            if (addedAttachment == null)
            {
                return new GeneralResult<AttachementDto>
                {
                    Success = false,
                    Errors = new[] { new ResultError { Code = "BugNotFound", Message = "Bug not found." } }
                };
            }
           
            await _unitOfWork.SaveChangesAsync();
            var resultDto = new AttachementDto
            {
                Id = addedAttachment.Id,
                FileName = addedAttachment.FileName,
                FilePath = addedAttachment.FilePath
            };

            return new GeneralResult<AttachementDto> { Success = true, Data = resultDto };
        }
    }
}
