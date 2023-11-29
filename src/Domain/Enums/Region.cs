using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(EnumerationJsonConverter<Region>))]
public class Region : Enumeration
{
    public Region(int id, string name) : base(id, name)
    {
    }

    public static readonly Region Bishkek = new(1, nameof(Bishkek));
    public static readonly Region Chui = new(2, nameof(Chui));
    public static readonly Region Batken = new(3, nameof(Batken));
    public static readonly Region JalalAbad = new(4, nameof(JalalAbad));
    public static readonly Region Naryn = new(5, nameof(Naryn));
    public static readonly Region Talas = new(6, nameof(Talas));
    public static readonly Region IssykKul = new(7, nameof(IssykKul));
    public static readonly Region Osh = new(8, nameof(Osh));
    public static readonly Region OshCity = new(9, nameof(OshCity));
}