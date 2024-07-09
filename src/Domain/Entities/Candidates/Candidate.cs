using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

/// <summary>
/// Candidate for government reward.
/// </summary>
public abstract class Candidate : BaseAuditableEntity
{
    [ForeignKey(nameof(CandidateType))]
    public string CandidateTypeId { get; set; } = null!;
    public CandidateType CandidateType { get; set; } = null!;
    [JsonIgnore]
    public byte[]? Image { get; set; }
    public string? ImageName { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    public string? AccompanyingLetterOutgoingNumber { get; set; }
    public DateTime? AccompanyingLetterOutgoingNumberRegistrationDate { get; set; } = DateTime.UtcNow.Date;
    public string? SpecialAchievements { get; set; }
    
    public ICollection<CandidateStatus> RewardApplicationStatuses { get; set; } = new List<CandidateStatus>();
    public ICollection<CandidateDocument> ApplicationDocuments { get; set; } = new List<CandidateDocument>();
}