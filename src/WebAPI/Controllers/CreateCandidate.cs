using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public partial class CandidatesController
{
    [HttpPost("[action]")]
    public async Task<ActionResult> CreateCandidate([FromBody]CreateCandidateArgument argument)
    {
        Candidate candidate = argument.CandidateTypeId switch
        {
            CandidateTypes.Entity => new Entity { EntityId = argument.MemberId, ModifiedBy = 1, CreatedBy = 1, },
            CandidateTypes.Citizen => new Citizen { PersonId = argument.MemberId, ModifiedBy = 1, CreatedBy = 1, },
            CandidateTypes.Foreigner => new Foreigner { PersonId = argument.MemberId, ModifiedBy = 1, CreatedBy = 1, },
            CandidateTypes.Mother => new Mother { PersonId = argument.MemberId, ModifiedBy = 1, CreatedBy = 1, },
            _ => throw new BadRequestException("Type not found")
        };
        candidate.Avatar = new Avatar();
        await _context.AddAsync(candidate);
        await _context.SaveChangesAsync();
        
        return Ok(candidate.Id);
    }

    public class CreateCandidateArgument
    {
        public int MemberId { get; set; }
        public required string CandidateTypeId { get; set; }
    }
}