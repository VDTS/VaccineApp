using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Area.Clinic;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

public class ClinicsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<ClinicModel> _clinics;
    public ClinicsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Clinics = new ObservableCollection<ClinicModel>();
        AddClinicCommand = new Command(AddClinic);
        Get();
    }

    public IEnumerable<ClinicModel> Clinics
    {
        get { return _clinics; }
        set
        {
            _clinics = value;
            OnPropertyChanged();
        }
    }
    public ICommand AddClinicCommand { private set; get; }
    private async void AddClinic(object obj)
    {
        var route = $"{nameof(AddClinicPage)}";
        await Shell.Current.GoToAsync(route);
    }
    private async Task Get()
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
}
