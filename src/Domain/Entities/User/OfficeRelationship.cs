namespace Domain.Entities;

/// <summary>
/// Self referencing many-to-many relationship.
/// </summary>
public class OfficeRelationship
{
    public int ChildOfficeId { get; set; }
    public Office? ChildOffice { get; set; }
    
    public int ParentOfficeId { get; set; }
    public Office? ParentOffice { get; set; }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((OfficeRelationship)obj);
    }

    protected bool Equals(OfficeRelationship other)
    {
        return ChildOfficeId == other.ChildOfficeId && ParentOfficeId == other.ParentOfficeId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ChildOfficeId, ParentOfficeId);
    }
}