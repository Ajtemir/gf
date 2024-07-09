using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// An application for government reward.
/// </summary>
public class Application : BaseAuditableEntity
{
    public int RewardId { get; set; }
    public Reward? Reward { get; set; }
    
    public Candidate Candidate { get; set; } = null!;
    
    public required string SpecialAchievements { get; set; }
    
    public ApplicationUser? CreatedByUser { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    
}