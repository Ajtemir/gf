using Newtonsoft.Json;

namespace WebAPI.Services.Integrator;

public class GetAuthInfocomHrm : BaseIntegrator<ArgumentCode, HrmEmployee>
{
    public GetAuthInfocomHrm(ArgumentCode arguments) : base(arguments)
    {
    }

    protected override string ClientId => "gosnagrada";
    protected override string Secret => "3c57eaaabfbb1a1c70e76bd882c33551";
    protected override string Service => throw new Exception("Error occured");
    public override async Task<HrmEmployee> ExecuteAsync()
    {
        HrmEmployee authInfocomHrm;
      try
      {
        FormUrlEncodedContent formContentAccessToken = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) new KeyValuePair<string, string>[6]
        {
          new KeyValuePair<string, string>("client_id", ClientId),
          new KeyValuePair<string, string>("client_secret", Secret),
          new KeyValuePair<string, string>("redirect_uri", _arguments.RedirectUrl),
          new KeyValuePair<string, string>("code", _arguments.Code),
          new KeyValuePair<string, string>("scope", "pin"),
          new KeyValuePair<string, string>("grant_type", "authorization_code")
        });
        HttpClient myHttpClientAccessToken = new HttpClient();
        HttpResponseMessage responseAccessTokent = await myHttpClientAccessToken.PostAsync("https://auth.infocom.kg/oauth/access_token", (HttpContent) formContentAccessToken);
        string stringContentAccessTokent = await responseAccessTokent.Content.ReadAsStringAsync();
        HrmAccessToken resultAccessTokent = JsonConvert.DeserializeObject<HrmAccessToken>(stringContentAccessTokent);
        FormUrlEncodedContent formContentEmployee = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) new KeyValuePair<string, string>[2]
        {
          new KeyValuePair<string, string>("client_id", ClientId),
          new KeyValuePair<string, string>("access_token", resultAccessTokent.AccessToken)
        });
        HttpClient myHttpClientEmployee = new HttpClient();
        HttpResponseMessage responseEmployee = await myHttpClientEmployee.PostAsync("https://auth.infocom.kg/api/employee", (HttpContent) formContentEmployee);
        string stringContentEmployee = await responseEmployee.Content.ReadAsStringAsync();
        HrmEmployee resultEmployee = JsonConvert.DeserializeObject<HrmEmployee>(stringContentEmployee);
        resultEmployee.Token = resultAccessTokent;
        authInfocomHrm = resultEmployee;
      }
      catch (Exception ex)
      {
        throw new Exception("Произошла ошибка в методе GetAuthInfocomHrm:" + ex.Message + " ");
      }
      return authInfocomHrm;
    }
}

public class ArgumentCode
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
        public string RedirectUrl { get; set; }
    }
    
    public class HrmEmployee
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("result")]
        public HrmEmployeeResult Employee { get; set; }

        public HrmAccessToken Token { get; set; }
    }
    
    public class HrmEmployeeResult
    {
        [JsonProperty("level_id")]
        public string Level_id { get; set; }

        [JsonProperty("employee_id")]
        public string Employee_id { get; set; }

        [JsonProperty("sname")]
        public string Sname { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fname")]
        public string Fname { get; set; }

        [JsonProperty("bdate")]
        public string Bdate { get; set; }

        [JsonProperty("pin")]
        public string Pin { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("state_id")]
        public string State_id { get; set; }

        [JsonProperty("state_name")]
        public string State_name { get; set; }

        [JsonProperty("main_state_id")]
        public string Main_state_id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("post_id")]
        public int Post_id { get; set; }

        [JsonProperty("post_name")]
        public string Post_name { get; set; }

        [JsonProperty("position_id")]
        public string Position_id { get; set; }

        [JsonProperty("position_name")]
        public string Position_name { get; set; }

        [JsonProperty("department_id")]
        public string Department_id { get; set; }

        [JsonProperty("department_name")]
        public string Department_name { get; set; }

        [JsonProperty("state_special_code")]
        public string StateSpecialCode { get; set; }

        [JsonProperty("edm_type")]
        public HrmEdmType EdmType { get; set; }

        [JsonProperty("state")]
        public HrmState State { get; set; }

        [JsonProperty("stateParent")]
        public HrmStateParent StateParent { get; set; }

        [JsonProperty("access_levels")]
        public string[] AccessLevels { get; set; }
    }
    
    public class HrmEdmType
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("isManager")]
        public bool? IsManager { get; set; }
    }
    
    public class HrmState
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("old_id")]
        public int? OldId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public HrmAddress Address { get; set; }

        [JsonProperty("coate")]
        public string Coate { get; set; }

        [JsonProperty("inn")]
        public string INN { get; set; }
    }
    
    public class HrmAddress
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("country_id")]
        public string Country_id { get; set; }

        [JsonProperty("region_id")]
        public string Region_id { get; set; }

        [JsonProperty("district_id")]
        public string District_id { get; set; }

        [JsonProperty("aymak_id")]
        public string Aymak_id { get; set; }

        [JsonProperty("village_id")]
        public string Village_id { get; set; }

        [JsonProperty("street_id")]
        public string Street_id { get; set; }

        [JsonProperty("house_id")]
        public string House_id { get; set; }

        [JsonProperty("house_txt")]
        public string House_txt { get; set; }

        [JsonProperty("flat_id")]
        public string Flat_id { get; set; }
    }
    
    public class HrmStateParent
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    
    public class HrmAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }