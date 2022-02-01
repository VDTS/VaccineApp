using VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

namespace VaccineApp.Views.Mobilizer.Home.Area.Clinic;

public partial class ClinicsListPage : ContentPage
{
    private readonly ClinicsListViewModel _viewModel;

    public ClinicsListPage(ClinicsListViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.Clear();
        _viewModel.Get();
		this.BindingContext = _viewModel;
    }
}
