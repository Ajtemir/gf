using Domain.Enums;

namespace Application.Common.Dto;

public class MotherDto : CandidateDto
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public string? Pin { get; set; }
    public string? PassportNumber { get; set; }
    public required Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly DeathDate { get; set; }
    
    public required string RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }
}