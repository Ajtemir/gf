using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class CandidateType
{
    [Key]
    public required string NameEn { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    public List<CandidateTypesDocumentTypes> CandidateTypesDocumentTypes { get; set; } = new();
}