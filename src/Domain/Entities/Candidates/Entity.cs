using System.ComponentModel.DataAnnotations.Schema;
using Domain.interfaces;

namespace Domain.Entities;

public class Entity : Candidate, IEntity, IUpdate<IEntity>
{
    [ForeignKey(nameof(PinEntity))]
    public int EntityId { get; set; }
    public PinEntity PinEntity { get; set; } = null!;
    public string NameRu { get; set; }
    public string NameKg { get; set; }
    public void Update(IEntity entity)
    {
        NameRu = entity.NameRu;
        NameKg = entity.NameKg;
    }
}