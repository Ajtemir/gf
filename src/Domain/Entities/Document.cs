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
    [ForeignKey(nameof(DocumentType))]
    public int DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; } = null!;
    [ForeignKey(nameof(RewardApplication))]
    public int RewardApplicationId { get; set; }
    [JsonIgnore]
    public RewardApplication RewardApplication { get; set; } = null!;

    public string? Extension => Path.GetExtension(this.Name)?.Remove(0);
    public string? ContentType => Extension is null ? null : $"application/${Extension}";

    public void Reset()
    {
        this.Bytes = null;
        this.Name = null;
    }
}