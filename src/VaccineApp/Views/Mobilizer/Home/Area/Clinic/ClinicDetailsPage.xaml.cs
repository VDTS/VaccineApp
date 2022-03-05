using Core.Models;

using Newtonsoft.Json;

using VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

namespace VaccineApp.Views.Mobilizer.Home.Area.Clinic;

[QueryProperty(nameof(Clinic), nameof(Clinic))]
public partial class ClinicDetailsPage : ContentPage
{
    private readonly ClinicDetailsViewModel _viewModel;
    public string Clinic { get; set; }
    public ClinicDetailsPage(ClinicDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        try
        {
            var clinic = JsonConvert.DeserializeObject<ClinicModel>(Clinic);
            _viewModel.GetQueryProperty(clinic);
            this.BindingContext = _viewModel;
        }
        catch (Exception)
        {
            return;
        }
    }
}