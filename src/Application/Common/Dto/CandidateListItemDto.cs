namespace Application.Common.Dto;

public class CandidateListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Pin { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CandidateType { get; set; }
}