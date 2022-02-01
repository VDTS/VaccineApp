using VaccineApp.ViewModels.Admin.Home.Cluster;

namespace VaccineApp.Views.Admin.Home.Cluster;
public partial class ClustersListPage : ContentPage
{
    private readonly ClustersListViewModel _viewModel;

    public ClustersListPage(ClustersListViewModel viewModel)
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