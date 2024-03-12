using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ChildDocumentType
{
    [Key, ForeignKey(nameof(DocumentType))]
    public int DocumentTypeId { get; set; }

    public DocumentType DocumentType { get; set; } = null!;
}