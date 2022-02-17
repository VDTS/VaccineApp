using Core.Enums;
using VaccineApp.Shells.Views;

namespace VaccineApp;

public partial class App : Application
{
	public App()
	{
		var role = Preferences.Get("Role", "AnonymousRole");

		InitializeComponent();

		MainPage = RolesList.Roles.Contains(role) ? new Appshell(role) : new Accessshell();
    }
}
