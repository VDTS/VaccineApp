using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

public class ClinicsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<ClinicModel> _clinics;
    public ClinicsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Clinics = new ObservableCollection<ClinicModel>();

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
