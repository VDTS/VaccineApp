using Auth.Factory;
using Core.Models;
using Newtonsoft.Json;

namespace Auth.Services;

public class AccountService
{
    private readonly IHttpClientFactory _clientFactory;

    public AccountService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    public async Task<string> ChangeAccountPassword(string token, string password)
    {
        var client = _clientFactory.CreateClient("authConf");

        var requestUri = String.Concat(
            "v1/accounts:update?key=",
            AuthConfigs.FirebaseApiKey);

        try
        {
            var identityModel = new ChangeAccountPasswordModel() { IdToken = token, Password = password, ReturnSecureToken = true };
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
                throw new Exception("Password not changed!");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> SendPasswordResetCode(string email)
    {
        var client = _clientFactory.CreateClient("authConf");

        var requestUri = String.Concat(
            "v1/accounts:sendOobCode?key=",
            AuthConfigs.FirebaseApiKey);

        try
        {
            var content = $"{{requestType: \"PASSWORD_RESET\", email: \"{email}\"}}";

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(requestUri, byteContent);

            if (s.IsSuccessStatusCode)
            {
                return await s.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Password reset email not sent");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> VerifyEmail(string idToken)
    {
        var client = _clientFactory.CreateClient("authConf");

        var requestUri = String.Concat(
            "v1/accounts:sendOobCode?key=",
            AuthConfigs.FirebaseApiKey);

        try
        {
            var content = $"{{requestType: \"VERIFY_EMAIL\", idToken: \"{idToken}\"}}";

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(requestUri, byteContent);

            if (s.IsSuccessStatusCode)
            {
                return await s.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Verification code not sent");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}