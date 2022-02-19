using VaccineApp.ViewModels.Parent.Guides;

namespace VaccineApp.Views.Parent.Guides;

public partial class VaccineGuidesPage : ContentPage
{
    private readonly VaccineGuidesViewModel _viewModel;

    public VaccineGuidesPage(VaccineGuidesViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.Get();
		this.BindingContext = _viewModel;
    }
}
