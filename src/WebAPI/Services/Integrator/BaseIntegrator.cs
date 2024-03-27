using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Application.Common.Exceptions;
using Newtonsoft.Json;

namespace WebAPI.Services.Integrator;

public abstract class BaseIntegrator<TArgument, TOutput> 
    where TArgument : class 
    where TOutput : class
{
    private readonly TArgument _arguments;
    private const string UrlAddress = "https://integrator2.srs.kg/ws/claim";

    protected BaseIntegrator(TArgument arguments)
    {
        _arguments = arguments;
    }
    protected virtual string ClientId => "ais_citizenship";
    protected virtual string Secret => "lYo63sIXyBsHZ17E";
    protected abstract string Service { get; }
    protected virtual Expression<Func<TOutput, object>>[]? RequiredJsonProperties => null;
    public async Task<TOutput> ExecuteAsync()
    {
        try
        {
            var elements = RequiredJsonProperties == null ? GetAllRequiredJsonProperties<TOutput>() : GetRequiredJsonProperties(RequiredJsonProperties);
            IntegratorBody<TArgument> integratorBodyGrnp = new IntegratorBody<TArgument>
            {
                ClientId = ClientId,
                Secret = Secret,
                Service = Service,
                Arguments = _arguments,
                Elements = elements,
            };

            using HttpClient httpClient = new();
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(UrlAddress, new StringContent(JsonConvert.SerializeObject(integratorBodyGrnp), Encoding.UTF8, "application/json"));
            httpResponseMessage.EnsureSuccessStatusCode();
            var responseBody = JsonConvert.DeserializeObject<TOutput>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            return responseBody;
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }

    private static List<string> GetRequiredJsonProperties<TClass>(params Expression<Func<TClass, object>>[] propertyLambda)
    {
        return propertyLambda
            .Select(x => (x.Body as MemberExpression)?.Member.GetCustomAttribute<JsonPropertyAttribute>().PropertyName)
            .Where(x => x != null).Distinct().ToList();
    }

    private static List<string?> GetAllRequiredJsonProperties<TClass>()
    {
        return typeof(TClass).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(propertyInfo => propertyInfo.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName)
            .Where(x => x != null).ToList();
    }
}
    
public class IntegratorBody<T>
{
    [JsonProperty("clientid")]
    public string ClientId { get; set; }

    [JsonProperty("secret")]
    public string Secret { get; set; }

    [JsonProperty("service")]
    public string Service { get; set; }

    [JsonProperty("arguments")]
    public T Arguments { get; set; }

    [JsonProperty("elements")]
    public List<string> Elements { get; set; }
}

public class ArgumentPin
{
    [JsonProperty("pin")]
    public long Pin { get; set; }
}