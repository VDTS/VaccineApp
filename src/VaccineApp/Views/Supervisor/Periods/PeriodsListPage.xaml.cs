using VaccineApp.ViewModels.Supervisor.Periods;

namespace VaccineApp.Views.Supervisor.Periods;

public partial class PeriodsListPage : ContentPage
{
    private readonly PeriodsListViewModel _viewModel;

    public PeriodsListPage(PeriodsListViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }
    protected override void OnAppearing()
    {
        this.BindingContext = _viewModel;
    }
}
