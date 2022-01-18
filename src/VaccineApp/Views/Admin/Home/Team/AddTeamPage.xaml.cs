using VaccineApp.ViewModels.Admin.Home.Team;

namespace VaccineApp.Views.Admin.Home.Team;
public partial class AddTeamPage : ContentPage
{
    public AddTeamPage(AddTeamViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}