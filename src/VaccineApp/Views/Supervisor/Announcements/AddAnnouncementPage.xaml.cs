using VaccineApp.ViewModels.Supervisor.Announcements;

namespace VaccineApp.Views.Supervisor.Announcements;

public partial class AddAnnouncementPage : ContentPage
{
	public AddAnnouncementPage(AddAnnouncementViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
