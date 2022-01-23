using VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;

namespace VaccineApp.Views.Mobilizer.Home.Area.Doctor;

public partial class AddDoctorPage : ContentPage
{
	public AddDoctorPage(AddDoctorViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
