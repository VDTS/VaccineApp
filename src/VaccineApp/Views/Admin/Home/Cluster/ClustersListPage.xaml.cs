using VaccineApp.ViewModels.Admin.Home.Cluster;

namespace VaccineApp.Views.Admin.Home.Cluster;
public partial class ClustersListPage : ContentPage
{
    public ClustersListPage(ClustersListViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}