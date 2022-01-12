using Auth.Factory;
using Core.Models;
using Newtonsoft.Json;

namespace Auth.Services;
public class SignInService
{
    private IHttpClientFactory _clientFactory;
    public SignInService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    public async Task<string> SignIn(string email, string password)
    {
        var client = _clientFactory.CreateClient();

        var requestUri = String.Concat(
            "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=",
            AuthConfigs.FirebaseApiKey);

        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            throw new Exception("No internet connection");
        }
        else
        {
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
    }
}
