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
        var application = new RewardApplication
        {
            RewardId = argument.RewardId,
            CandidateId = argument.CandidateId,
            SpecialAchievements = argument.SpecialAchievements,
        };
        await _context.RewardApplications.AddAsync(application);
        await _context.SaveChangesAsync();
        
        var documents = await _context.RewardDocumentTypes.Where(x=>x.RewardId == application.RewardId)
            .Select(x=>new Document
        {
            DocumentTypeId = x.DocumentTypeId,
            RewardApplicationId = application.Id,
        }).ToListAsync();

        var createdStatus = new RewardApplicationStatus
        {
            RewardApplicationId = application.Id,
            Status = RewardApplicationStatusType.Saved,
            ChangeDate = DateTime.Now.SetKindUtc(),
            PreviousStatusId = null,
            OfficeId = 51617,
        };
        
        await _context.Documents.AddRangeAsync(documents);
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