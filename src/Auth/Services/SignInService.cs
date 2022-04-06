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
            var identityModel = new SignInModel() { Email = email, Password = password, ReturnSecureToken = true };
            var content = JsonConvert.SerializeObject(identityModel);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(requestUri, byteContent);

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
            IdTokenModel accessToken = new() { IdToken = idToken };
            var content = JsonConvert.SerializeObject(accessToken);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(requestUri, byteContent);

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
