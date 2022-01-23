using VaccineApp.ViewModels.App.Profile;

namespace VaccineApp.Views.App.Profile;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}