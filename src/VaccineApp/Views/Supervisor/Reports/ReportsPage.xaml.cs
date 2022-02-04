using VaccineApp.ViewModels.Supervisor.Reports;

namespace VaccineApp.Views.Supervisor.Reports;

public partial class ReportsPage : ContentPage
{
    private readonly ReportsViewModel _viewModel;

    public ReportsPage(ReportsViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.GetClusters();
        await _viewModel.GetTeams();

		this.BindingContext = _viewModel;
    }
}
