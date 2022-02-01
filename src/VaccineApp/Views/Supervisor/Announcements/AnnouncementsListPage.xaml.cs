using VaccineApp.ViewModels.Supervisor.Announcements;

namespace VaccineApp.Views.Supervisor.Announcements;

public partial class AnnouncementsListPage : ContentPage
{
    private readonly AnnouncementsListViewModel _viewModel;

    public AnnouncementsListPage(AnnouncementsListViewModel viewModel)
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
