using VaccineApp.ViewModels.Parent;

namespace VaccineApp.Views.Parent;

public partial class ParentFamilyPage : ContentPage
{
	public ParentFamilyPage(ParentFamilyViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
