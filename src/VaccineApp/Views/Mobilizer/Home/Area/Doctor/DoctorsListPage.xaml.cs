using VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;

namespace VaccineApp.Views.Mobilizer.Home.Area.Doctor;

public partial class DoctorsListPage : ContentPage
{
    private readonly DoctorsListViewModel _viewModel;

    public DoctorsListPage(DoctorsListViewModel viewModel)
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
