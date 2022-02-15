using VaccineApp.ViewModels.Parent.Guides;

namespace VaccineApp.Views.Parent.Guides;

public partial class VaccinesTimeTablePage : ContentPage
{
	public VaccinesTimeTablePage(VaccinesTimeTableViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
