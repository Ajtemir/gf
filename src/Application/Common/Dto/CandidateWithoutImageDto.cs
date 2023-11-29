namespace Application.Common.Dto;

public class CandidateWithoutImageDto
{
    public int Id { get; set; }

    public required string CandidateType { get; set; }
    public required string Name { get; set; }

    public int CreatedBy { get; set; }
    public string? CreatedByUser { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ModifiedBy { get; set; }
    public string? ModifiedByUser { get; set; }
    public DateTime ModifiedAt { get; set; }
}