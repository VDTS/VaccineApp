using VaccineApp.ViewModels.Mobilizer.Home.Area.School;

namespace VaccineApp.Views.Mobilizer.Home.Area.School;

public partial class SchoolsListPage : ContentPage
{
    private readonly SchoolsListViewModel _viewModel;

    public SchoolsListPage(SchoolsListViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Clear();
        _viewModel.Get();
		this.BindingContext = _viewModel;
    }
}
