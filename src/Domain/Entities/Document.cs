using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(RewardApplicationId), nameof(DocumentTypeId), IsUnique = true)]
public class Document
{
    public int Id { get; set; }
    [JsonIgnore]
    public byte[]? Bytes { get; set; }
    public string? Name { get; set; }
    public string? Extension { get; set; }
    public string? ContentType { get; set; }
    [ForeignKey(nameof(DocumentType))]
    public int DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; } = null!;
    [ForeignKey(nameof(RewardApplication))]
    public int RewardApplicationId { get; set; }
    [JsonIgnore]
    public RewardApplication RewardApplication { get; set; } = null!;
}