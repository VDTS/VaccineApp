using VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;

namespace VaccineApp.Views.Mobilizer.Home.Area.Doctor;

public partial class DoctorsListPage : ContentPage
{
	public DoctorsListPage(DoctorsListViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
