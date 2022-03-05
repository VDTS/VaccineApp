using Core.Models;

using Newtonsoft.Json;

using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

namespace VaccineApp.Views.Mobilizer.Home.Area.Masjeed;

[QueryProperty(nameof(Masjeed), nameof(Masjeed))]
public partial class MasjeedDetailsPage : ContentPage
{
    private readonly MasjeedDetailsViewModel _viewModel;

    public string Masjeed { get; set; }
    public MasjeedDetailsPage(MasjeedDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        try
        {
            var masjeed = JsonConvert.DeserializeObject<MasjeedModel>(Masjeed);
            _viewModel.GetQueryProperty(masjeed);
            this.BindingContext = _viewModel;
        }
        catch (Exception)
        {
            return;
        }
    }
}