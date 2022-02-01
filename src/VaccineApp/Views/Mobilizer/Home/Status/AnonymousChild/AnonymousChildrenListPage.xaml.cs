using VaccineApp.ViewModels.Mobilizer.Home.Status.AnonymousChild;

namespace VaccineApp.Views.Mobilizer.Home.Status.AnonymousChild;

public partial class AnonymousChildrenListPage : ContentPage
{
    private readonly AnonymousChildrenListViewModel _viewModel;

    public AnonymousChildrenListPage(AnonymousChildrenListViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        _viewModel.Clear();
        _viewModel.Get();
        this.BindingContext = _viewModel;        
    }
}
