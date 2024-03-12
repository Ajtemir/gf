using System.ComponentModel.DataAnnotations.Schema;
using Domain.Dictionary;

namespace Domain.Entities;

public class Foreigner : PersonCandidate
{
    public int CitizenshipId { get; set; }
    public Citizenship? Citizenship { get; set; }
}