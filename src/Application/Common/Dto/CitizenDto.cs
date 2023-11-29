using Domain.Enums;

namespace Application.Common.Dto;

public class CitizenDto : CandidateDto
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public string? Pin { get; set; }
    public string? PassportNumber { get; set; }
    public required Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    
    public required string RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }
    public int YearsOfWorkTotal { get; set; }
    public int YearsOfWorkInIndustry { get; set; }
    public int YearsOfWorkInCollective { get; set; }
    public int EducationId { get; set; }
    public string? Education { get; set; }
    public string? ScienceDegree { get; set; }
}