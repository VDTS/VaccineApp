using Core.Models;

using Newtonsoft.Json;

using VaccineApp.ViewModels.Mobilizer.Home.Status.Vaccine;

namespace VaccineApp.Views.Mobilizer.Home.Status.Vaccine;

[QueryProperty(nameof(Vaccine), nameof(Vaccine))]
public partial class VaccineDetailsPage : ContentPage
{
    private readonly VaccineDetailsViewModel _viewModel;

    public string Vaccine { get; set; }
    public VaccineDetailsPage(VaccineDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        try
        {
            var vaccine = JsonConvert.DeserializeObject<VaccineModel>(Vaccine);
            _viewModel.GetQueryProperty(vaccine);
            this.BindingContext = _viewModel;
        }
        catch (Exception)
        {
            return;
        }
    }
}