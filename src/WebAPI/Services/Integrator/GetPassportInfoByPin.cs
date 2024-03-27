using Newtonsoft.Json;

namespace WebAPI.Services.Integrator;

public class GetPassportInfoByPin : BaseIntegrator<ArgumentPin, PassportInfo>
{
    protected override string Service => "get-passport-data-by-pin";
    public GetPassportInfoByPin(ArgumentPin arguments) : base(arguments)
    {
    }
}


public class PassportInfo
{
    [JsonProperty("surname")]
    public string Lastname { get; set; }

    [JsonProperty("name")]
    public string Firstname { get; set; }

    [JsonProperty("patronymic")]
    public string Patronymic { get; set; }

    [JsonProperty("pin")]
    public string Pin { get; set; }

    [JsonProperty("passport_series")]
    public string? PassportSeries { get; set; }

    [JsonProperty("passport_number")]
    public string? PassportNumber { get; set; }

    [JsonProperty("issued_date")]
    public DateParse IssuedDate { get; set; }

    [JsonProperty("passport_authority")]
    public string PassportAuthority { get; set; }

    [JsonProperty("expired_date")]
    public DateParse ExpiredDate { get; set; }
}

public class DateParse
{
    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("timezone_type")]
    public int TimezoneType { get; set; }

    [JsonProperty("timezone")]
    public string Timezone { get; set; }
}