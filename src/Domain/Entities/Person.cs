using Domain.Enums;

namespace Domain.Entities;

public abstract class Person : Candidate
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    
    public required Gender Gender { get; set; }
    
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
}