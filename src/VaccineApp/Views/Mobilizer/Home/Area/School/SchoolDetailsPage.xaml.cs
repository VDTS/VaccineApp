using Core.Models;

using Newtonsoft.Json;

using VaccineApp.ViewModels.Mobilizer.Home.Area.School;

namespace VaccineApp.Views.Mobilizer.Home.Area.School;

[QueryProperty(nameof(School), nameof(School))]
public partial class SchoolDetailsPage : ContentPage
{
    private readonly SchoolDetailsViewModel _viewModel;

    public string School { get; set; }
    public SchoolDetailsPage(SchoolDetailsViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        var school = JsonConvert.DeserializeObject<SchoolModel>(School);
        _viewModel.GetQueryProperty(school);
        this.BindingContext = _viewModel;
    }
}