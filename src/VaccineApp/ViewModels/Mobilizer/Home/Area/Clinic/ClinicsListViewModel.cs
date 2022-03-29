using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using VaccineApp.Views.Mobilizer.Home.Area.Clinic;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

public partial class ClinicsListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    IEnumerable<ClinicModel> _clinics;

    [ObservableProperty]
    ClinicModel _selectedClinic;
    public ClinicsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Clinics = new ObservableCollection<ClinicModel>();
    }

    [ICommand]
    async void AddClinic(object obj)
    {
        var route = $"{nameof(AddClinicPage)}";
        await Shell.Current.GoToAsync(route);
    }

    [ICommand]
    async void ClinicDetails()
    {
        if (SelectedClinic == null)
        {
            return;
        }
        else
        {
            var SelectedItemJson = JsonConvert.SerializeObject(SelectedClinic);
            var route = $"{nameof(ClinicDetailsPage)}?Clinic={SelectedItemJson}";
            await Shell.Current.GoToAsync(route);
            SelectedClinic = null;
        }
    }

    public async void Get()
    {
        try
        {
            Clinics = await _unitOfWork.GetClinics();
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        Clinics = new ObservableCollection<ClinicModel>();
    }
}
