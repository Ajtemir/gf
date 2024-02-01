using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public partial class ApplicationsController
{
    [HttpPost("Create")]
    public async Task<ActionResult> CreateDocument([FromBody]CreateApplicationArgument argument)
    {
        var validation = await new CreateApplicationArgumentValidator().ValidateAsync(argument);
        if (!validation.IsValid)
        {
            throw new BadRequestException(validation.ToString());
        }
        var application = new RewardApplication
        {
            RewardId = argument.RewardId,
            CandidateId = argument.CandidateId,
            SpecialAchievements = argument.SpecialAchievements,
        };
        _context.RewardApplications.Add(application);
        await _context.SaveChangesAsync();
        return Ok(application);
    }
    
    public class CreateApplicationArgument
    {
        public int CandidateId { get; set; }
        public int RewardId { get; set; }
        public required string SpecialAchievements { get; set; }
    }
    
    public class CreateApplicationArgumentValidator : AbstractValidator<CreateApplicationArgument>
    {
        public CreateApplicationArgumentValidator()
        {
            RuleFor(x => x.CandidateId).NotEmpty();
            RuleFor(x => x.RewardId).NotEmpty();
            RuleFor(x => x.SpecialAchievements).NotEmpty().MinimumLength(10).MinimumLength(1);
        }
    }
}