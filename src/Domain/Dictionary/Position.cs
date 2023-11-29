using Domain.Common;

namespace Domain.Dictionary;

public class Position : BaseEntity
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
}