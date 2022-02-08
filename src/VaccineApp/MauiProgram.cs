using Auth.Factory;
using Core.Factory;
using DAL.Factory;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
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
            .RegisterBlazorMauiWebView()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            })
            .Host.
            ConfigureAppConfiguration((app, config) =>
            {
                var assembly = typeof(App).GetTypeInfo().Assembly;
                config.AddJsonFile(new EmbeddedFileProvider(assembly), "AppConfigs.SettingsDefaultsValues.json", optional: false, false);
                config.AddJsonFile(new EmbeddedFileProvider(assembly), "AppConfigs.AppSettings.json", optional: false, true);
                config.AddJsonFile(new EmbeddedFileProvider(assembly), "SecretFiles.AppSecrets.json", optional: false, true);
            });

        builder.Services.AddBlazorWebView();

        builder.Services.Configure<AppSecrets>(builder.Configuration.GetSection("AppSecrets"));
        builder.Services.Configure<SettingsDefaultsValues>(builder.Configuration.GetSection("SettingsDefaultsValues"));
        builder.Services.AddViewModels();

        builder.Services.AddDAL(
            options =>
            {
                if (builder.Configuration.GetSection("AppSettings:Env").Value == "online")
                {
                    options.FirebaseApiKey = builder.Configuration.GetSection("AppSecrets:FirebaseApiKey").Value;
                    options.FirebaseBaseAddress = builder.Configuration.GetSection("AppSecrets:FirebaseBaseAddress").Value;
                }
                else if (builder.Configuration.GetSection("AppSettings:Env").Value == "offline")
                {
                    options.FirebaseApiKey = builder.Configuration.GetSection("AppSecrets:FirebaseApiKey").Value;
                    options.FirebaseBaseAddress = builder.Configuration.GetSection("AppSecrets:FirebaseBaseAddress_Offline").Value;
                }
            }
        );

        builder.Services.AddCoreServices();

        builder.Services.AddAuth(
            options =>
            {
                options.FirebaseApiKey = builder.Configuration.GetSection("AppSecrets:FirebaseApiKey").Value;
            }
        );

        builder.Services.AddRealCache();

        return builder.Build();
    }
}
