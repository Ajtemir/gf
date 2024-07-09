using System.ComponentModel.DataAnnotations.Schema;
using Domain.Dictionary;
using Domain.interfaces;

namespace Domain.Entities;

public class Foreigner : PersonCandidate, IForeigner, IUpdate<IForeigner>
{
    public int? CitizenshipId { get; set; }
    public Citizenship? Citizenship { get; set; }
    public void Update(IForeigner entity)
    {
        CitizenshipId = entity.CitizenshipId;
    }
}