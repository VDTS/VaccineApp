using VaccineApp.ViewModels.Mobilizer.Announcements;

namespace VaccineApp.Views.Mobilizer.Announcements;

public partial class AnnouncementListPage : ContentPage
{
    private readonly AnnouncementListViewModel _viewModel;

    public AnnouncementListPage(AnnouncementListViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        _viewModel.Get();
		this.BindingContext = _viewModel;
    }
}
