using Domain.Dictionary;

namespace Domain.Entities;

public class Foreigner : Person
{
    public int CitizenshipId { get; set; }
    public Citizenship? Citizenship { get; set; }
}