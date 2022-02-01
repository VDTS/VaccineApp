using VaccineApp.ViewModels.Supervisor;

namespace VaccineApp.Views.Supervisor;

public partial class StatsPage : ContentPage
{
    private readonly StatsViewModel _viewModel;

    public StatsPage(StatsViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Clear();
        _viewModel.GetCluster();
		this.BindingContext = _viewModel;
    }
}
