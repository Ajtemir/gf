using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey(nameof(CandidateTypeId), nameof(DocumentTypeId))]
public class CandidateTypesDocumentTypes
{
    [ForeignKey(nameof(CandidateType))]
    public string CandidateTypeId { get; set; }
    public required CandidateType CandidateType { get; set; }
    [ForeignKey(nameof(DocumentType))]
    public int DocumentTypeId { get; set; }
    public required DocumentType DocumentType { get; set; }
}