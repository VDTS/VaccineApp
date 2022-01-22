using VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

namespace VaccineApp.Views.Mobilizer.Home.Area.Clinic;

public partial class ClinicsListPage : ContentPage
{
	public ClinicsListPage(ClinicsListViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
