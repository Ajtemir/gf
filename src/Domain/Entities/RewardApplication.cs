using System.ComponentModel.DataAnnotations.Schema;
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
    
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; } = null!;
    
    // public required Region Region { get; set; }
    public required string SpecialAchievements { get; set; }
    
    public ApplicationUser? CreatedByUser { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    public ICollection<RewardApplicationStatus> RewardApplicationStatuses { get; set; } = new List<RewardApplicationStatus>();
    // [ForeignKey(nameof(CandidateType))]
    // public required string CandidateTypeId { get; set; }
    //
    // public CandidateType CandidateType { get; set; } = null!;
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}