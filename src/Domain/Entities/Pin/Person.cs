using Domain.Enums;

namespace Domain.Entities;

public class Person : Member
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public required Gender Gender { get; set; }
    
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? BirthPlace { get; set; }
    public string? PassportSeries { get; set; }
    public required string? PassportNumber { get; set; }
    public required string? RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }
}