namespace Domain.Entities;

public class CandidateTypesDocumentTypes
{
    public int CandidateTypeId { get; set; }
    public required CandidateType CandidateType { get; set; }
    public int DocumentTypeId { get; set; }
    public required DocumentType DocumentType { get; set; }
}