using Core.Models;

using Newtonsoft.Json;

using VaccineApp.ViewModels.Parent;

namespace VaccineApp.Views.Parent;

[QueryProperty(nameof(Child), nameof(Child))]
public partial class ParentChildDetailsPage : ContentPage
{
    private readonly ParentChildDetailsViewModel _viewModel;
    public string Child { get; set; }

    public ParentChildDetailsPage(ParentChildDetailsViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        var child = JsonConvert.DeserializeObject<ChildModel>(Child);
        _viewModel.GetQueryProperty(child);
        _viewModel.GetVaccines();
        this.BindingContext = _viewModel;
    }
}