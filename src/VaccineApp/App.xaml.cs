using Core.Enums;
using DAL.Persistence;
using VaccineApp.Shells.Views;

namespace VaccineApp;

public partial class App : Application
{
	public App(UnitOfWork unitOfWork)
	{
		var role = Preferences.Get("Role", "AnonymousRole");

		InitializeComponent();

        unitOfWork.AddClaims(
            Preferences.Get("ClusterId", null!),
            Preferences.Get("TeamId", null!),
            Preferences.Get("FamilyId", null!));

        MainPage = RolesList.Roles.Contains(role) ? new Appshell(role) : new Accessshell();
    }
}
