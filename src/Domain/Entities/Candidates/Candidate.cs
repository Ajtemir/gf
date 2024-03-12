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
    
    public byte[]? Image { get; set; }
    public string? ImageName { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    [JsonIgnore]
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}