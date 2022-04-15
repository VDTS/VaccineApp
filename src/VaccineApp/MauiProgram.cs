using Auth.Factory;
using Core.Factory;
using DAL.Factory;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using RealCache.Factory;
using System.Reflection;
using VaccineApp.AppConfigs;
using VaccineApp.Factory;

namespace VaccineApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("VaccineApp.AppConfigs.SettingsDefaultsValues.json"))
            builder.Configuration.AddJsonStream(resource);

        using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("VaccineApp.AppConfigs.AppSettings.json"))
            builder.Configuration.AddJsonStream(resource);

        using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("VaccineApp.SecretFiles.AppSecrets.json"))
            builder.Configuration.AddJsonStream(resource);

        builder.Services.Configure<AppSecrets>(builder.Configuration.GetSection("AppSecrets"));
        builder.Services.Configure<SettingsDefaultsValues>(builder.Configuration.GetSection("SettingsDefaultsValues"));

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddViewModels();
        builder.Services.AddPages();
        builder.Services.AddDeviceSpecificFeatures();
        builder.Services.AddFeatures();

        builder.Services.AddDAL(
            options =>
            {
                if (builder.Configuration.GetSection("AppSettings:Env").Value == "online")
                {
                    options.FirebaseApiKey = builder.Configuration.GetSection("AppSecrets:FirebaseApiKey").Value;
                    options.FirebaseBaseAddress = builder.Configuration.GetSection("AppSecrets:FirebaseBaseAddress").Value;
                    options.Env = builder.Configuration.GetSection("AppSettings:Env").Value;
                }
                else if (builder.Configuration.GetSection("AppSettings:Env").Value == "offline")
                {
                    options.FirebaseApiKey = builder.Configuration.GetSection("AppSecrets:FirebaseApiKey").Value;
                    options.FirebaseBaseAddress = builder.Configuration.GetSection("AppSecrets:FirebaseBaseAddress_Offline").Value;
                    options.Env = builder.Configuration.GetSection("AppSettings:Env").Value;
                }
            }
        );

        builder.Services.AddCoreServices();

        builder.Services.AddAuth(
            options =>
            {
                if (builder.Configuration.GetSection("AppSettings:Env").Value == "online")
                {
                    options.FirebaseApiKey = builder.Configuration.GetSection("AppSecrets:FirebaseApiKey").Value;
                    options.FirebaseAuthAddress = builder.Configuration.GetSection("AppSecrets:FirebaseAuthAddress").Value;
                }
                else if (builder.Configuration.GetSection("AppSettings:Env").Value == "offline")
                {
                    options.FirebaseApiKey = builder.Configuration.GetSection("AppSecrets:FirebaseApiKey").Value;
                    options.FirebaseAuthAddress = builder.Configuration.GetSection("AppSecrets:FirebaseAuthAddress_Offline").Value;
                }
            }
        );

        builder.Services.AddRealCache();

        // Create FirebaseApp
        using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("VaccineApp.SecretFiles.firebase_private_key.json"))
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromStream(resource)
            });

        return builder.Build();
    }
}
