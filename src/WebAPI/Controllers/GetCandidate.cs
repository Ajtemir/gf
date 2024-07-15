using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;


public partial class CandidatesController
{
    [HttpGet("[action]/{candidateId:int}")]
    public async Task<ActionResult> GetCandidate(int candidateId)
    {
        var candidate = await _context.PersonCandidates.FirstOrErrorAsync(x => x.Id == candidateId);
        return Ok(new
        {
            candidateType = candidate.CandidateTypeId,
            candidate.Id,
            candidate.PersonId,
            candidate.AvatarId,
        });
    }
}