using Core.Models;
using Newtonsoft.Json;
using VaccineApp.ViewModels.Mobilizer.Home.Status;

namespace VaccineApp.Views.Mobilizer.Home.Status;

[QueryProperty(nameof(Child), nameof(Child))]
public partial class ChildDetailsPage : ContentPage
{
    private readonly ChildDetailsViewModel _viewModel;
    public string Child { get; set; }
    public ChildDetailsPage(ChildDetailsViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        var child = JsonConvert.DeserializeObject<ChildModel>(Child);
        _viewModel.GetQueryProperty(child);
        _viewModel.Get();
        this.BindingContext = _viewModel;
    }
}
