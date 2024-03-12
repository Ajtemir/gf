using Application.Common.Extensions;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

public partial class ApplicationsController
{
    [HttpPost("Create")]
    public async Task<ActionResult> CreateDocument([FromBody]CreateApplicationArgument argument)
    {
        var application = new Domain.Entities.Application
        {
            RewardId = argument.RewardId,
            CandidateId = argument.CandidateId,
            SpecialAchievements = argument.SpecialAchievements,
        };
        await _context.RewardApplications.AddAsync(application);
        await _context.SaveChangesAsync();
        var officeId = UserOfficeId;
        var createdStatus = new ApplicationStatus
        {
            ChangeDate = DateTime.Now.SetKindUtc(),
            ApplicationId = application.Id,
            OfficeId = officeId,
            PreviousStatusId = null,
            Status = ApplicationStatusType.Saved,
        };
        await _context.RewardApplicationStatuses.AddAsync(createdStatus);
        await _context.SaveChangesAsync();
        
        var documents = await _context.RewardDocumentTypes.Where(x=>x.RewardId == application.RewardId)
            .Select(x => new Document
        {
            DocumentTypeId = x.DocumentTypeId,
        }).ToListAsync();

        await _context.Documents.AddRangeAsync(documents);
        await _context.SaveChangesAsync();
        await _context.ApplicationDocuments.AddRangeAsync(documents.Select(x=> new ApplicationDocument
        {
            ApplicationId = application.Id,
            DocumentId = x.Id,
        }));
        await _context.SaveChangesAsync();
        return Ok(new CreateApplicationResult
        {
            Id = application.Id,
        });
    }
    
    public class CreateApplicationArgument
    {
        public int CandidateId { get; set; }
        public int RewardId { get; set; }
        public required string SpecialAchievements { get; set; }
    }

    public class CreateApplicationResult
    {
        public int Id { get; set; }
    }
    
    public class CreateApplicationArgumentValidator : AbstractValidator<CreateApplicationArgument>
    {
        public CreateApplicationArgumentValidator()
        {
            RuleFor(x => x.CandidateId).NotEmpty();
            RuleFor(x => x.RewardId).NotEmpty();
            RuleFor(x => x.SpecialAchievements).NotEmpty();
        }
    }
}