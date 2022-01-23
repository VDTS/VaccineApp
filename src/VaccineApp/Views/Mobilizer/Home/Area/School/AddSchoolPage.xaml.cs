using VaccineApp.ViewModels.Mobilizer.Home.Area.School;

namespace VaccineApp.Views.Mobilizer.Home.Area.School;

public partial class AddSchoolPage : ContentPage
{
	public AddSchoolPage(AddSchoolViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
