namespace Domain.Common;

/// <summary>
/// Base entity includes create and modify information.
/// </summary>
public class BaseAuditableEntity : BaseEntity
{
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ModifiedBy { get; set; }
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
}