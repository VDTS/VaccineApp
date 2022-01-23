using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

namespace VaccineApp.Views.Mobilizer.Home.Area.Masjeed;

public partial class AddMasjeedPage : ContentPage
{
	public AddMasjeedPage(AddMasjeedViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
