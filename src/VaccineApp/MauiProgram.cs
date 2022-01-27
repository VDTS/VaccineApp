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

		builder.Configuration.AddUserSecrets<AppSettings>();
		builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
		builder.Services.Configure<FirebasePrivateKey>(builder.Configuration.GetSection("FirebasePrivateKey"));
		builder.Services.Configure<SettingsDefaultsValues>(builder.Configuration.GetSection("SettingsDefaultsValues"));
		builder.Services.AddViewModels();

		builder.Services.AddDAL(
			options => {
				options.FirebaseApiKey = builder.Configuration.GetSection("AppSettings:FirebaseApiKey").Value;
				options.FirebaseBaseAddress = builder.Configuration.GetSection("AppSettings:FirebaseBaseAddress").Value;
			}
		);

		builder.Services.AddCoreServices();

		builder.Services.AddAuth(
			options =>
            {
				options.FirebaseApiKey = builder.Configuration.GetSection("AppSettings:FirebaseApiKey").Value;
			}
		);

		return builder.Build();
	}
}
