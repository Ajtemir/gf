using Domain.Common;
using Domain.Entities;

namespace Domain.Dictionary;

public class Education : BaseEntity
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }

    public ICollection<Citizen> Citizens { get; set; } = new List<Citizen>();
}