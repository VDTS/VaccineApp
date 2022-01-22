using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;

public class DoctorsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<DoctorModel> _doctors;
    public DoctorsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Doctors = new ObservableCollection<DoctorModel>();

        Get();
    }

    public IEnumerable<DoctorModel> Doctors
    {
        get { return _doctors; }
        set
        {
            _doctors = value;
            OnPropertyChanged();
        }
    }


    private async Task Get()
    {
        try
        {
            Doctors = await _unitOfWork.GetDoctors();
        }
        catch (Exception)
        {
            return;
        }
    }
}
