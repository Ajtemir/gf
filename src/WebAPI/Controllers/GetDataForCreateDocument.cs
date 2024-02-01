using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class ApplicationsController
{
    [HttpGet("{candidateId:int}")]
    public async Task<ActionResult> GetDataForCreateDocument(int candidateId)
    {
        Candidate candidate = await _context.Candidates
            .Include(x=>x.CandidateType)
            .ThenInclude(x=>x.CandidateTypesRewards)
            .ThenInclude(x=>x.Reward)
            .FirstOrErrorAsync(x => x.Id == candidateId, "Candidate by id not found");
        var rewards = candidate.CandidateType.CandidateTypesRewards.Select(x => x.Reward).ToList();
        var response = new GetDataForCreateDocumentResult
        {
            Rewards = rewards,
            
        };
        return Ok(response);
    }

    public class GetDataForCreateDocumentResult
    {
        public List<Reward> Rewards { get; set; }
    }
}