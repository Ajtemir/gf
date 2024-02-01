namespace Domain.Entities;

public class Child : Person
{
    public MothersChildren MothersChildren { get; set; } = null!;
}