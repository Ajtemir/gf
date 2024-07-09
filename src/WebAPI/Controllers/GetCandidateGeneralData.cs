using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class CandidatesController
{
    [HttpGet("[action]/{personCandidateId:int}")]
    public async Task<ActionResult> GetCandidateGeneralData(int personCandidateId)
    {
        var personCandidate = await _context.PersonCandidates.Select(x=> new
        {
            PersonCandidateId = x.Id,
            x.AccompanyingLetterOutgoingNumber,
            x.AccompanyingLetterOutgoingNumberRegistrationDate,
            x.SpecialAchievements,
            x.PositionKg,
            x.PositionRu,
        }).FirstOrErrorAsync(x => x.PersonCandidateId == personCandidateId);
        await _context.SaveChangesAsync();
        return Ok(personCandidate);
    }
}