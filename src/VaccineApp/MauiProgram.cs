using Auth.Factory;
using Core.Factory;
using DAL.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
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
			})
			.Host.
			ConfigureAppConfiguration((app, config) =>
            {
				var assembly = typeof(App).GetTypeInfo().Assembly;
				config.AddJsonFile(new EmbeddedFileProvider(assembly), "AppConfigs.SettingsDefaultsValues.json", optional: false, false);
            });

		builder.Configuration.AddUserSecrets<AppSecrets>();
		builder.Services.Configure<AppSecrets>(builder.Configuration.GetSection("AppSecrets"));
		builder.Services.Configure<SettingsDefaultsValues>(builder.Configuration.GetSection("SettingsDefaultsValues"));
		builder.Services.AddViewModels();

		builder.Services.AddDAL(
			options => {
				options.FirebaseApiKey = builder.Configuration.GetSection("AppSecrets:FirebaseApiKey").Value;
				options.FirebaseBaseAddress = builder.Configuration.GetSection("AppSecrets:FirebaseBaseAddress").Value;
			}
		);

		builder.Services.AddCoreServices();

		builder.Services.AddAuth(
			options =>
            {
				options.FirebaseApiKey = builder.Configuration.GetSection("AppSecrets:FirebaseApiKey").Value;
			}
		);

		return builder.Build();
	}
}
