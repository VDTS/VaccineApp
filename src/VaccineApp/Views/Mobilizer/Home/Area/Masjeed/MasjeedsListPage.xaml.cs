using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

namespace VaccineApp.Views.Mobilizer.Home.Area.Masjeed;
public partial class MasjeedsListPage : ContentPage
{
    private readonly MasjeedListViewModel _viewModel;

    public MasjeedsListPage(MasjeedListViewModel viewModel)
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