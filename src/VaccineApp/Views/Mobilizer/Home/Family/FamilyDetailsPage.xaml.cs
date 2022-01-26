using Core.Models;
using Newtonsoft.Json;
using VaccineApp.ViewModels.Mobilizer.Home.Family;

namespace VaccineApp.Views.Mobilizer.Home.Family;

[QueryProperty(nameof(Family), nameof(Family))]
public partial class FamilyDetailsPage : ContentPage
{
    private readonly FamilyDetailsViewModel _viewModel;

    public string Family { get; set; }
    public FamilyDetailsPage(FamilyDetailsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        var result = JsonConvert.DeserializeObject<FamilyModel>(Family);
        _viewModel.GetQueryProperty(result);
        this.BindingContext = _viewModel;
    }
}
