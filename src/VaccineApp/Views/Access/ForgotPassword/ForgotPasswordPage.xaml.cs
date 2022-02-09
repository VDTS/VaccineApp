using VaccineApp.ViewModels.Access.ForgotPassword;

namespace VaccineApp.Views.Access.ForgotPassword;

public partial class ForgotPasswordPage : ContentPage
{
	public ForgotPasswordPage(ForgotPasswordViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
