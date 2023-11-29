using Domain.Common;

namespace Domain.Entities;

/// <summary>
/// Candidate for government reward.
/// </summary>
public abstract class Candidate : BaseAuditableEntity
{
    public string CandidateType { get; set; } = null!;
    
    public byte[]? Image { get; set; }
    public string? ImageName { get; set; }
    
    public ApplicationUser? CreatedByUser { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    
    public ICollection<RewardApplication> Applications { get; set; } = new List<RewardApplication>();
}