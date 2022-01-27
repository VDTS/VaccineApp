using VaccineApp.ViewModels.App.AboutUs;

namespace VaccineApp.Views.App.AboutUs;

public partial class AboutUsPage : ContentPage
{
	public AboutUsPage(AboutUsViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
