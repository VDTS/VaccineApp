using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;

namespace VaccineApp;

public partial class App : Application
{
	public App(MasjeedsListPage masjeedListPage)
	{
		InitializeComponent();

		MainPage = masjeedListPage;
	}
}
