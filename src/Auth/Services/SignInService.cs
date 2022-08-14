using Auth.Factory;
using Core.Models;
using Newtonsoft.Json;

namespace Auth.Services;
public class SignInService
{
    readonly IHttpClientFactory _clientFactory;
    public SignInService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    public async Task<string> SignIn(string email, string password)
    {
        var client = _clientFactory.CreateClient("authConf");

        var requestUri = String.Concat(
            "v1/accounts:signInWithPassword?key=",
            AuthConfigs.FirebaseApiKey);

        try
        {
            var parameters = new Dictionary<string, string>
                {
                    {"email", email},
                    {"password", password},
                    {"returnSecureToken", "true"}
                };

            var urlEncodedParameters = new FormUrlEncodedContent(parameters);
            var s = await client.PostAsync(requestUri, urlEncodedParameters);

            if (s.IsSuccessStatusCode)
            {
                return await s.Content.ReadAsStringAsync();
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

    public async Task<string> AccountInfoLookup(string idToken)
    {
        var client = _clientFactory.CreateClient("authConf");

        var requestUri = String.Concat(
            "v1/accounts:lookup?key=",
            AuthConfigs.FirebaseApiKey);

        try
        {
            var parameters = new Dictionary<string, string>
                {
                    {"idToken", idToken}
                };

            var urlEncodedParameters = new FormUrlEncodedContent(parameters);
            var s = await client.PostAsync(requestUri, urlEncodedParameters);

            if (s.IsSuccessStatusCode)
            {
                return await s.Content.ReadAsStringAsync();
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
}
