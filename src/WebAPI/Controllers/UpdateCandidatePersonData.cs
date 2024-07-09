using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class CandidatesController
{
    [HttpPost("[action]/{personId:int}")]
    public async Task<ActionResult> UpdateCandidateGeneralData(int personId, [FromBody]UpdateCandidatePersonDataArgument argument)
    {
        var personCandidate = await _context.PersonCandidates.FirstOrErrorAsync(x => x.PersonId == personId);
        personCandidate.PositionKg = argument.PositionKg;
        personCandidate.PositionRu = argument.PositionRu;
        personCandidate.AccompanyingLetterOutgoingNumber = argument.AccompanyingLetterOutgoingNumber;
        personCandidate.AccompanyingLetterOutgoingNumberRegistrationDate = argument.AccompanyingLetterOutgoingNumberRegistrationDate;
        personCandidate.SpecialAchievements = argument.SpecialAchievements;
        await _context.SaveChangesAsync();
        return Ok(personCandidate);
    }

    public class UpdateCandidatePersonDataArgument
    {
        public string PositionRu { get; set; }
        public string PositionKg { get; set; }
        public string AccompanyingLetterOutgoingNumber { get; set; }
        public DateTime AccompanyingLetterOutgoingNumberRegistrationDate { get; set; }
        public string SpecialAchievements { get; set; }
    }
}