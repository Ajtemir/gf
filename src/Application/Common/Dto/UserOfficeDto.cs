namespace Application.Common.Dto;

public class UserOfficeDto
{
    public required int OfficeId { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }

    protected bool Equals(UserOfficeDto other)
    {
        return OfficeId == other.OfficeId && NameRu == other.NameRu && NameKg == other.NameKg;
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

        return Equals((UserOfficeDto)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OfficeId, NameRu, NameKg);
    }
}