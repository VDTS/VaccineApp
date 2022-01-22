using VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;

namespace VaccineApp.Views.Mobilizer.Home.Area.Influencer;

public partial class InfluencersListPage : ContentPage
{
	public InfluencersListPage(InfluencersListViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
