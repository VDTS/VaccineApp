using VaccineApp.ViewModels.Mobilizer.Home.Status;

namespace VaccineApp.Views.Mobilizer.Home.Status;

public partial class StatusPage : ContentPage
{
    private readonly StatusViewModel _viewModel;

    public StatusPage(StatusViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        _viewModel.Clear();
        _viewModel.Get();
        this.BindingContext = _viewModel;
    }
}
