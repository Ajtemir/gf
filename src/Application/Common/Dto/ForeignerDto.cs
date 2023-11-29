using Domain.Enums;

namespace Application.Common.Dto;

public class ForeignerDto : CandidateDto
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public required Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public int CitizenshipId { get; set; }
    public string? CitizenshipRu { get; set; }
    public string? CitizenshipKg { get; set; }
}