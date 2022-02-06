using Core.Models;
using Newtonsoft.Json;
using VaccineApp.ViewModels.Mobilizer.Home.Status.Vaccine;

namespace VaccineApp.Views.Mobilizer.Home.Status.Vaccine;

[QueryProperty(nameof(ChildId), nameof(ChildId))]
public partial class AddVaccinePage : ContentPage
{
    private readonly AddVaccineViewModel _viewModel;

    public string ChildId { get; set; }
	public AddVaccinePage(AddVaccineViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();	
		_viewModel.GetQueryProperty(ChildId);
		this.BindingContext = _viewModel;
    }
}
