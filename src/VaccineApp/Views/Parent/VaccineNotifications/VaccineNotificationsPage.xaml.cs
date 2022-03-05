
using VaccineApp.ViewModels.Parent.VaccineNotifications;

namespace VaccineApp.Views.Parent.VaccineNotifications;

public partial class VaccineNotificationsPage : ContentPage
{
    private readonly VaccinesNotificationsViewModel _viewModel;

    public VaccineNotificationsPage(VaccinesNotificationsViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        await _viewModel.GetParentFamily();
        await _viewModel.GetChilds();
        this.BindingContext = _viewModel;
    }
}