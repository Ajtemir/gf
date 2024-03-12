using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Entity : Candidate
{
    [ForeignKey(nameof(PinEntity))]
    public int EntityId { get; set; }
    public PinEntity PinEntity { get; set; } = null!;
    public string NameRu { get; set; }
    public string NameKg { get; set; }
}