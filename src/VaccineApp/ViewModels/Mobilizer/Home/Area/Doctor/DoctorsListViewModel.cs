using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Area.Doctor;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;

public class DoctorsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<DoctorModel> _doctors;
    public DoctorsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Doctors = new ObservableCollection<DoctorModel>();
        AddDoctorCommand = new Command(AddDoctor);
        Get();
    }
    public ICommand AddDoctorCommand { private set; get; }
    public IEnumerable<DoctorModel> Doctors
    {
        get { return _doctors; }
        set
        {
            _doctors = value;
            OnPropertyChanged();
        }
    }
    private async void AddDoctor(object obj)
    {
        var route = $"{nameof(AddDoctorPage)}";
        await Shell.Current.GoToAsync(route);
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
