using VaccineApp.ViewModels.Mobilizer.Home.Family;

namespace VaccineApp.Views.Mobilizer.Home.Family;
public partial class FamiliesListPage : ContentPage
{
    private readonly FamiliesListViewModel _viewModel;

    public FamiliesListPage(FamiliesListViewModel viewModel)
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