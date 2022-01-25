using Core.Models;
using Newtonsoft.Json;
using VaccineApp.ViewModels.App.Profile;

namespace VaccineApp.Views.App.Profile;

[QueryProperty(nameof(Profile), nameof(Profile))]
public partial class EditProfilePage : ContentPage
{
    private readonly EditProfileViewModel _viewModel;

    public EditProfilePage(EditProfileViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
		var result = JsonConvert.DeserializeObject<ProfileModel>(Profile);
        _viewModel.GetQueryProperty(result);
		this.BindingContext = _viewModel;
	}
    public string Profile { get; set; }
}
