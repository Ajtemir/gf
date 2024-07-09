using Domain.Common;

namespace Domain.Entities;

/// <summary>
/// Office from EKyzmat. Id have semantic meaning, therefore should not be generated.
/// </summary>
public class Office : BaseAuditableEntity
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }

    public ICollection<OfficeRelationship> ChildOffices { get; set; } = new List<OfficeRelationship>();
    public ICollection<OfficeRelationship> ParentOffices { get; set; } = new List<OfficeRelationship>();

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    public ICollection<UserOffice> UserOffices { get; set; } = new List<UserOffice>();
    public ICollection<CandidateStatus> RewardApplicationStatuses { get; set; } = new List<CandidateStatus>();

    public ApplicationUser? CreatedByUser { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
}
