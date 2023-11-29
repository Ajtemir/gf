using Domain.Common;

namespace Domain.Entities;

public class Reward : BaseAuditableEntity
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public required string ImageName { get; set; }
    public required byte[] Image { get; set; }
    
    public ApplicationUser? CreatedByUser { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
}