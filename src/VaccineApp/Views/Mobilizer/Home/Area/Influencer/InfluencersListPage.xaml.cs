using VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;

namespace VaccineApp.Views.Mobilizer.Home.Area.Influencer;

public partial class InfluencersListPage : ContentPage
{
    private readonly InfluencersListViewModel _viewModel;

    public InfluencersListPage(InfluencersListViewModel viewModel)
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
