namespace Domain.Entities;

public class CandidateType
{
    public int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    public List<CandidateTypesDocumentTypes> CandidateTypesDocumentTypes { get; set; } = new();
}