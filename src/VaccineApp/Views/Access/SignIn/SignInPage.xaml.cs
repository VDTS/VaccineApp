using VaccineApp.ViewModels.Access.SignIn;

namespace VaccineApp.Views.Access.SignIn;
public partial class SignInPage : ContentPage
{
    public SignInPage(SignInViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}