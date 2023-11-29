namespace Domain.Entities;

/// <summary>
/// Represents join table for many-to-many relationship between
/// <see cref="ApplicationUser"/> and <see cref="Office"/>.
/// </summary>
public class UserOffice
{
    public int UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public int OfficeId { get; set; }
    public Office? Office { get; set; }

    protected bool Equals(UserOffice other)
    {
        return UserId == other.UserId && OfficeId == other.OfficeId;
    }

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

        return Equals((UserOffice)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, OfficeId);
    }
}