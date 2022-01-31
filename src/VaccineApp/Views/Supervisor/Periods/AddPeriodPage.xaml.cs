using VaccineApp.ViewModels.Supervisor.Periods;

namespace VaccineApp.Views.Supervisor.Periods;

public partial class AddPeriodPage : ContentPage
{
	public AddPeriodPage(AddPeriodViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
