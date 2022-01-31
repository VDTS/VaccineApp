using VaccineApp.ViewModels.Mobilizer.Home.Status.AnonymousChild;

namespace VaccineApp.Views.Mobilizer.Home.Status.AnonymousChild;

public partial class AddAnonymousChildPage : ContentPage
{
	public AddAnonymousChildPage(AddAnonymousChildViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
