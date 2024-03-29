using VaccineApp.ViewModels.Admin.Home.User;

namespace VaccineApp.Views.Admin.User;

public partial class UsersListPage : ContentPage
{
    private readonly UsersListViewModel _viewModel;

    public UsersListPage(UsersListViewModel viewModel)
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
