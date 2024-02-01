using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class DocumentType
{
    [Key]
    public int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    public bool Required { get; set; } = false;

    public ICollection<RewardDocumentType> RewardDocumentTypes { get; set; } = new List<RewardDocumentType>();
}