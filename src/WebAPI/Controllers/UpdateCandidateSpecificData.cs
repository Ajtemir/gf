using Domain.Dictionary;
using Domain.Enums;
using Domain.interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers;


public partial class CandidatesController
{
    [HttpPost("[action]/{candidateId:int}")]
    public async Task<ActionResult> UpdateCandidateSpecificData(int candidateId, [FromBody]UpdateCandidateSpecificDataArgument argument)
    {
        var candidateType = await _context.Candidates.Where(x => x.Id == candidateId).Select(x=> x.CandidateTypeId).FirstOrErrorAsync();
        switch (candidateType)
        {
            case CandidateTypes.Citizen:
                var citizen = await _context.Citizens.FirstOrErrorAsync(x => x.Id == candidateId);
                citizen.Update(argument);
                break;
            case CandidateTypes.Mother:
                break;
            case CandidateTypes.Foreigner:
                var foreigner = await _context.Foreigners.FirstOrErrorAsync(x => x.Id == candidateId);
                foreigner.Update(argument);
                break;                
            case CandidateTypes.Entity:
                var entity = await _context.Entities.FirstOrErrorAsync(x => x.Id == candidateId);
                entity.Update(argument);
                break;
        }
        await _context.SaveChangesAsync();
        return Ok();
    }

    public class UpdateCandidateSpecificDataArgument : ICitizen, IForeigner, IEntity, IMother
    {
        public int? EducationId { get; set; }
        public string? ScienceDegree { get; set; }
        public int YearsOfWorkTotal { get; set; }
        public int YearsOfWorkInCollective { get; set; }
        public int YearsOfWorkInIndustry { get; set; }
        public int? CitizenshipId { get; set; }
        public string? NameKg { get; set; }
        public string? NameRu { get; set; }
    }
    
}