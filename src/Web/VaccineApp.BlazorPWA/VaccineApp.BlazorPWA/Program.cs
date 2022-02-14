using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VaccineApp.BlazorPWA;
using DAL.Factory;
using Core.Factory;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var jsonFilesHttpClient = new HttpClient(){
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};

using var appSettingsResponse = await jsonFilesHttpClient.GetAsync("AppConfigs/AppSettings.json");
using var appSettingsstream = await appSettingsResponse.Content.ReadAsStreamAsync();
builder.Configuration.AddJsonStream(appSettingsstream);

using var appSecretsResponse = await jsonFilesHttpClient.GetAsync("SecretFiles/AppSecrets.json");
using var appSecretsStream = await appSecretsResponse.Content.ReadAsStreamAsync();
builder.Configuration.AddJsonStream(appSecretsStream);

// Adding DAL Library
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

await builder.Build().RunAsync();
