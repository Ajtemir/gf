namespace Domain.Entities;

public class Child : Person
{
    public ICollection<MothersChildren> MothersChildren { get; set; } = new List<MothersChildren>();
}