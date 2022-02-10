using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Reflection;
using VaccineApp.Shells.Views;

namespace VaccineApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		InitFirebaseApp();
		SelectShell();
    }

    private void InitFirebaseApp()
    {
        // This code copies Embededd file to Cache.
        var cacheFile = Path.Combine(FileSystem.CacheDirectory, "firebase_private_key.json");
        if (File.Exists(cacheFile))
            File.Delete(cacheFile);
        using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("VaccineApp.SecretFiles.firebase_private_key.json"))
        using (var file = new FileStream(cacheFile, FileMode.Create, FileAccess.Write))
        {
            resource.CopyTo(file);
        }

        // This code creates FirebaseApp
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(cacheFile)
        });
    }

    private async void SelectShell()
    {
		var idToken = await SecureStorage.GetAsync("IdToken");
		var role = Preferences.Get("Role", "AnonymousRole");

		if (!string.IsNullOrEmpty(idToken))
		{
			Current.MainPage = new Appshell(role);
		}
		else
		{
			Current.MainPage = new Accessshell();
		}
	}
}
