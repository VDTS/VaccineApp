using Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Factory;
public static class AuthConfigurationExtensions
{
    public static IServiceCollection AddAuth(
        this IServiceCollection service,
        Action<AuthSettings> Configure)
    {
        var appSettings = new AuthSettings();

        Configure(appSettings);

        AuthConfigs.FirebaseApiKey = appSettings.FirebaseApiKey;

        service.AddHttpClient();
        service.AddScoped<SignInService>();
        service.AddScoped<AccountService>();

        return service;
    }
}
