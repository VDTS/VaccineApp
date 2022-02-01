using VaccineApp.ViewModels.Admin.Home.Team;

namespace VaccineApp.Views.Admin.Home.Team;
public partial class TeamsListPage : ContentPage
{
    private readonly TeamsListViewModel _viewModel;

    public TeamsListPage(TeamsListViewModel viewModel)
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