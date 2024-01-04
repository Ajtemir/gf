namespace Domain.Entities;

public class DocumentType
{
    public int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    public bool Required { get; set; } = false;

    public List<CandidateTypesDocumentTypes> CandidateTypesDocumentTypes { get; set; } = new();
}