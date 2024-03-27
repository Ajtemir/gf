using Newtonsoft.Json;

namespace WebAPI.Services.Integrator;

public class GetZagsByPin : BaseIntegrator<ArgumentPin, ZagsInfo>
{
    public GetZagsByPin(ArgumentPin arguments) : base(arguments)
    {
    }

    protected override string Service => "get-zags-data-by-pin";
}

public class ZagsInfo
{
    [JsonProperty("pin")]
    public long Pin { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }

    [JsonProperty("pinBlocked")]
    public bool PinBlocked { get; set; }

    [JsonProperty("maritalStatus")]
    public string MaritalStatus { get; set; }

    [JsonProperty("maritalStatusId")]
    public short? MaritalStatusId { get; set; }

    [JsonProperty("surname")]
    public string Surname { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("patronymic")]
    public string? Patronymic { get; set; }

    [JsonProperty("dateOfBirth")] 
    public DateParse Birthdate { get; set; }

    [JsonProperty("deathDate")]
    public DateParse? DeathDate { get; set; }

    [JsonProperty("citizenship")]
    public string? Citizenship { get; set; }

    [JsonProperty("citizenshipId")]
    public int? CitizenshipId { get; set; }

    [JsonProperty("gender")]
    public int Gender { get; set; }

    [JsonProperty("nationality")]
    public string Nationality { get; set; }

    [JsonProperty("nationalityId")]
    public int? NationalityId { get; set; }
}