using Newtonsoft.Json;

namespace WebAPI.Services.Integrator;

public class GetZagsChildren : BaseIntegrator<ArgumentPin, GetZagsChildrenResult>
{
    public GetZagsChildren(ArgumentPin arguments) : base(arguments)
    {
    }

    protected override string ClientId => "state_award";
    protected override string Secret => "orkf8mv72y";
    protected override string Service => "get-zags-children";
}

public class GetZagsChildrenResult
{
    [JsonProperty("children")]
    public List<ChildZags> Children { get; set; }
}

public class ChildZags
{
    [JsonProperty("pin")]
    public string Pin { get; set; }
}