namespace Domain.Entities;

public class Child : Person
{
    public MotherChild MotherChild { get; set; } = null!;
    public ICollection<ChildDocument> ChildDocuments { get; set; } = new List<ChildDocument>();
}