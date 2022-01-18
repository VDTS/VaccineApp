using VaccineApp.ViewModels.Admin.Home.Team;

namespace VaccineApp.Views.Admin.Home.Team;
public partial class TeamsListPage : ContentPage
{
    public TeamsListPage(TeamsListViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}