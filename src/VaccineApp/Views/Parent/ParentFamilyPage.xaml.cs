using VaccineApp.ViewModels.Parent;

namespace VaccineApp.Views.Parent;

public partial class ParentFamilyPage : ContentPage
{
    private readonly ParentFamilyViewModel _viewModel;

    public ParentFamilyPage(ParentFamilyViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
		this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        _viewModel.GetFamily();
    }
}
