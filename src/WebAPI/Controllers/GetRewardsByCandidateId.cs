using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class RewardsController
{
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<ActionResult> GetRewardsByCandidateId([FromQuery]int candidateId)
    {
        var candidate = await _context.Candidates.FirstOrErrorAsync(x => x.Id == candidateId);
        var rewards = await _context.CandidateTypesRewards.Include(x => x.Reward)
            .Where(x => x.CandidateTypeId == candidate.CandidateTypeId).Select(x=>x.Reward).ToListAsync();
        return Ok(rewards.Select(x=>(GetRewardsByCandidateIdResult)x));
    }

    public class GetRewardsByCandidateIdResult
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string NameKg { get; set; }
        
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Image { get; set; }
        public string ImageName { get; set; }
        
        public static explicit operator GetRewardsByCandidateIdResult(Reward reward) => new GetRewardsByCandidateIdResult
        {
            NameRu = reward.NameRu,
            Id = reward.Id,
            NameKg = reward.NameKg,
            Image = Convert.ToBase64String(reward.Image),
            ImageName = reward.ImageName,
            ModifiedAt = reward.ModifiedAt,
            CreatedAt = reward.CreatedAt,
            CreatedBy = reward.CreatedBy,
            ModifiedBy = reward.ModifiedBy,
        };
    }
}