using Newtonsoft.Json;

namespace WebAPI.Services.Integrator;

public class GetPhotoByPin : BaseIntegrator<ArgumentPin, PhotoList>
{
    public GetPhotoByPin(ArgumentPin arguments) : base(arguments)
    {
    }

    protected override string Service => "photo-by-pin";
    protected override string ClientId => "svr";
    protected override string Secret => "tQcvdoQEbwbmaW7y";
}

public class PhotoList
{
    [JsonProperty("PhotoList")] public IEnumerable<PhotoData> Photos { get; set; }
}
    

public class PhotoData
{
    [JsonProperty("person_image")] public string? PersonImage { get; set; }
    [JsonProperty("formular_number")] public string FormularNumber { get; set; }
    [JsonProperty("person_lastname_kyr")] public string LastNameKg { get; set; }
    [JsonProperty("person_firstname_kyr")] public string FirstNameKg { get; set; }
    [JsonProperty("person_patronymic_kyr")] public string PatronymicNameKg { get; set; }
    [JsonProperty("person_idnp")] public string PersonIdnp { get; set; }
}