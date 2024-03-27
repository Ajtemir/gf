using Newtonsoft.Json;

namespace WebAPI.Services.Integrator;

public class GetAddressByPin : BaseIntegrator<ArgumentPin, AsbAddress>
{
    public GetAddressByPin(ArgumentPin arguments) : base(arguments)
    {
    }

    protected override string ClientId => "svr";
    protected override string Secret => "tQcvdoQEbwbmaW7y";

    protected override string Service => "get-address-registred-battues-birthplace";
}

public class AsbAddress
{
    [JsonProperty("success")] public bool Success { get; set; }
    [JsonProperty("data")] public AsbAddressData Data { get; set; }
}

public class AsbAddressData
{
    [JsonProperty("currentAddress")] public AsbAddressDataCurrentAddress CurrentAddress { get; set; }
    [JsonProperty("addressBirthPlace")] public AsbAddressBirthPlace AddressBirthPlace { get; set; }
}

public class AsbAddressDataCurrentAddress
{
    [JsonProperty("address")] public string Address { get; set; }
    [JsonProperty("record_date")] public string RecordDate { get; set; }
    [JsonProperty("status")] public bool Status { get; set; }
    [JsonProperty("write_passport")] public int WritePassport { get; set; }
    [JsonProperty("write_passport_date")] public DateTime WritePassportDate { get; set; }
    [JsonProperty("registered_date")] public string RegisteredDate { get; set; }
}

public class AsbAddressBirthPlace
{
    [JsonProperty("address")] public string Address { get; set; }
    [JsonProperty("record_date")] public string RecordDate { get; set; }
    [JsonProperty("status")] public bool Status { get; set; }
}