using Core.Models;
using DAL.Persistence;

using Newtonsoft.Json;

using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Area.Clinic;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

public class ClinicsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<ClinicModel> _clinics;
    private ClinicModel _selectedClinic;
    public ClinicsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Clinics = new ObservableCollection<ClinicModel>();
        AddClinicCommand = new Command(AddClinic);
        ClinicDetailsCommand = new Command(ClinicDetails);
    }

    public ClinicModel SelectedClinic
    {
        get { return _selectedClinic; }
        set { _selectedClinic = value; OnPropertyChanged(); }
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
    public ICommand ClinicDetailsCommand { private set; get; }
    private async void AddClinic(object obj)
    {
        var route = $"{nameof(AddClinicPage)}";
        await Shell.Current.GoToAsync(route);
    }

    private async void ClinicDetails()
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
