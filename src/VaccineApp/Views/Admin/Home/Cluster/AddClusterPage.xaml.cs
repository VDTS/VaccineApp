using VaccineApp.ViewModels.Admin.Home.Cluster;

namespace VaccineApp.Views.Admin.Home.Cluster;
public partial class AddClusterPage : ContentPage
{
    public AddClusterPage(AddClusterViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}