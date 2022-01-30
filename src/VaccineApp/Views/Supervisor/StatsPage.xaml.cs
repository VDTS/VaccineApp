using VaccineApp.ViewModels.Supervisor;

namespace VaccineApp.Views.Supervisor;

public partial class StatsPage : ContentPage
{
	public StatsPage(StatsViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
