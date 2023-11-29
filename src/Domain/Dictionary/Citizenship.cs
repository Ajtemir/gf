using Domain.Common;
using Domain.Entities;

namespace Domain.Dictionary;

public class Citizenship : BaseEntity
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }

    public ICollection<Foreigner> Foreigners { get; set; } = new List<Foreigner>();
}