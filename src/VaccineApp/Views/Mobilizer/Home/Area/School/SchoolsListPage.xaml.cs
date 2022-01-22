using VaccineApp.ViewModels.Mobilizer.Home.Area.School;

namespace VaccineApp.Views.Mobilizer.Home.Area.School;

public partial class SchoolsListPage : ContentPage
{
	public SchoolsListPage(SchoolsListViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
