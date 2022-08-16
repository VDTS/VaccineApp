using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace DAL.Persistence;
public class DbNodePath
{
    // Token management

    public string IdToken()
    {
        if (UnitOfWork._idToken != null)
        {
            var jwthandler = new JwtSecurityTokenHandler();
            var jwttoken = jwthandler.ReadToken(UnitOfWork._idToken as string);
            var expDate = jwttoken.ValidTo;

            if (expDate < DateTime.UtcNow.AddMinutes(1))
                return RefreshTheToken().Result;
            else
                return UnitOfWork._idToken as string;
        }
        else
        {
            return RefreshTheToken().Result;

        }
    }

    public async Task<string> RefreshTheToken()
    {

        var client = new HttpClient();

        var requestUri = String.Concat(
            "https://securetoken.googleapis.com/v1/token?key=",
            DALConfigs.FirebaseApiKey);

        try
        {
            var parameters = new Dictionary<string, string>
                {
                    {"grant_type", "refresh_token"},
                    {"refresh_token", UnitOfWork._refreshToken}
                };

            var urlEncodedParameters = new FormUrlEncodedContent(parameters);
            var s = await client.PostAsync(requestUri, urlEncodedParameters);

            if (s.IsSuccessStatusCode)
            {
                var jResponse = await s.Content.ReadAsStringAsync();
                var jData = JsonConvert.DeserializeObject<JObject>(jResponse);
                var accessToken = jData?.GetValue("access_token")?.ToString();

                UnitOfWork._idToken = accessToken is not null ? accessToken : "";
                return accessToken is not null ? accessToken : "";
            }
            else
            {
                throw new Exception("Not authenticated");
            }
        }
        catch (Exception)
        {
            throw;
        }

    }
    // This method return string starts with ? to add this parameter
    public string ParamsForEnvironment()
    {
        if (DALConfigs.Env == "offline")
        {
            return "?ns=vaccineapp-2022";
        }
        else if (DALConfigs.Env == "online")
        {
            return $"?auth={IdToken()}";
        }
        else
        {
            return "";
        }
    }

    // This method return string starts with & to add to other parameters
    public string AddParamsForEnvironment()
    {
        if (DALConfigs.Env == "offline")
        {
            return "&ns=vaccineapp-2022";
        }
        else if (DALConfigs.Env == "online")
        {
            return $"?auth={IdToken()}";
        }
        else
        {
            return "";
        }
    }
    // VaccineApp NoSQL Schema

    // Cluster
    //      ClusterIds

    // ClusterIds
    //      TeamIds

    // TeamId
    //      Masjeeds
    //      Family
    //              FamilyIds

    // FamilyId
    //      ChildIds

    // ChildId
    //      VaccineIds

    public string Masjeed(string teamId) => $"Masjeed/{teamId}.json{ParamsForEnvironment()}";
    public string Child(string familyId) => $"Child/{familyId}.json{ParamsForEnvironment()}";
    public string Cluster() => $"Cluster.json{ParamsForEnvironment()}";
    public string Team(string Id)
    {
        return $"Team/{Id}.json{ParamsForEnvironment()}";
    }
    public string Family(string teamId)
    {
        return $"Family/{teamId}.json{ParamsForEnvironment()}";
    }
    public string Clinic(string teamId)
    {
        return $"Clinic/{teamId}.json{ParamsForEnvironment()}";
    }
    public string Doctor(string teamId)
    {
        return $"Doctor/{teamId}.json{ParamsForEnvironment()}";
    }
    public string Influencer(string teamId)
    {
        return $"Influencer/{teamId}.json{ParamsForEnvironment()}";
    }
    public string School(string teamId)
    {
        return $"School/{teamId}.json{ParamsForEnvironment()}";
    }

    public string Announcement() => $"Announcement.json{ParamsForEnvironment()}";
    public string Periods() => $"Period.json{ParamsForEnvironment()}";
    public string AnonymousChild(string teamId) => $"AnonymousChild{teamId}.json{ParamsForEnvironment()}";
    public string Vaccine(string childId) => $"Vaccine/{childId}.json{ParamsForEnvironment()}";


    public string ActivePeriods() => $"{Periods()}?orderBy=\"IsActive\"&equalTo=true{AddParamsForEnvironment()}";
    public string PeriodActiveField(string periodFID) => $"Period/{periodFID}/IsActive.json{ParamsForEnvironment()}";

}
