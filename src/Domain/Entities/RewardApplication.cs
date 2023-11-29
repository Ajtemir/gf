using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// An application for government reward.
/// </summary>
public class RewardApplication : BaseAuditableEntity
{
    public int RewardId { get; set; }
    public Reward? Reward { get; set; }
    
    public int RewardCandidateId { get; set; }
    public Candidate? RewardCandidate { get; set; }
    
    public required Region Region { get; set; }
    public required string SpecialAchievements { get; set; }
    
    public ApplicationUser? CreatedByUser { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
}