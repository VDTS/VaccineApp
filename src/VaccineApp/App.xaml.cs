using VaccineApp.Shells.Views;

namespace VaccineApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		SelectShell();
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
