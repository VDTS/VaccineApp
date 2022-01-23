using VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

namespace VaccineApp.Views.Mobilizer.Home.Area.Clinic;

public partial class AddClinicPage : ContentPage
{
	public AddClinicPage(AddClinicViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
