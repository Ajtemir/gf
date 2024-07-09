using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class CandidatesController
{
    [HttpGet("[action]/{candidateId:int}")]
    public async Task<ActionResult> GetCandidateAvatar(int candidateId)
    {
        var personCandidate = await _context.Candidates.Select(x=> new
        {
            x.Id,
            x.Image,
            x.ImageName,
        }).FirstOrErrorAsync(x => x.Id == candidateId);
        return Ok(personCandidate);
    }
}