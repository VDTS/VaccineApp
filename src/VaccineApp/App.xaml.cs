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
			Preferences.Get("ClusterId", "AnonymousCluster"),
			Preferences.Get("TeamId", "AnonymousTeam"),
			Preferences.Get("FamilyId", "AnonymousFamily"));

		MainPage = RolesList.Roles.Contains(role) ? new Appshell(role) : new Accessshell();
    }
}
