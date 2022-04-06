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
        AuthConfigs.FirebaseAuthAddress = appSettings.FirebaseAuthAddress;

        service.AddScoped<SignInService>();
        service.AddScoped<AccountService>();

        if(AuthConfigs.FirebaseAuthAddress != null)
            service.AddHttpClient("authConf", c =>
            {
                c.BaseAddress = new Uri(AuthConfigs.FirebaseAuthAddress);
            });

        return service;
    }
}
