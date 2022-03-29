using System.Collections.ObjectModel;
using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;
using VaccineApp.Views.Mobilizer.Home.Status.Vaccine;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status;

public partial class ChildDetailsViewModel : ObservableObject
{

    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    ChildModel _child;

    [ObservableProperty]
    IEnumerable<VaccineModel> _vaccines;

    [ObservableProperty]
    VaccineModel _selectedVaccine;

    public ChildDetailsViewModel(UnitOfWork unitOfWork)
    {
        Vaccines = new ObservableCollection<VaccineModel>();
        _unitOfWork = unitOfWork;
    }
    public void GetQueryProperty(ChildModel child)
    {
        Child = child;
    }

    [ICommand]
    async void AddVaccine()
    {
        var route = $"{nameof(AddVaccinePage)}?ChildId={Child.Id.ToString()}";
        await Shell.Current.GoToAsync(route);
    }

    [ICommand]
    async void VaccineDetails()
    {
        if (SelectedVaccine == null)
        {
            return;
        }
        else
        {
            var SelectedItemJson = JsonConvert.SerializeObject(SelectedVaccine);
            var route = $"{nameof(VaccineDetailsPage)}?Vaccine={SelectedItemJson}";
            await Shell.Current.GoToAsync(route);
            SelectedVaccine = null;
        }
    }

    public async void GetVaccines()
    {
        try
        {
            Vaccines = await _unitOfWork.GetVaccines(Child.Id.ToString());
        }
        catch (Exception)
        {
            return;
        }
    }
}
