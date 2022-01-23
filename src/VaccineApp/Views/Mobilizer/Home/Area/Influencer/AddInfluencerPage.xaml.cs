using VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;

namespace VaccineApp.Views.Mobilizer.Home.Area.Influencer;

public partial class AddInfluencerPage : ContentPage
{
	public AddInfluencerPage(AddInfluencerViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
