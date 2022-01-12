using Auth.Factory;
using DAL.Factory;
using Microsoft.Extensions.Configuration;
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

		builder.Configuration.AddUserSecrets<AppSettings>();
		builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

		builder.Services.AddViewModels();
		builder.Services.AddViewPages();

		builder.Services.AddDAL(
			options => {
				options.FirebaseApiKey = builder.Configuration.GetSection("AppSettings:FirebaseApiKey").Value;
				options.FirebaseBaseAddress = builder.Configuration.GetSection("AppSettings:FirebaseBaseAddress").Value;
			}
		);

		builder.Services.AddAuth(
			options =>
            {
				options.FirebaseApiKey = builder.Configuration.GetSection("AppSettings:FirebaseApiKey").Value;
			}
		);

		return builder.Build();
	}
}
