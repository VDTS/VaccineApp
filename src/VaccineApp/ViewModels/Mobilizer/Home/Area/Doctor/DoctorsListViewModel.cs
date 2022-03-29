using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.Views.Mobilizer.Home.Area.Doctor;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;

public partial class DoctorsListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    IEnumerable<DoctorModel> _doctors;
    public DoctorsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Doctors = new ObservableCollection<DoctorModel>();
    }

    [ICommand]
    async void AddDoctor(object obj)
    {
        var route = $"{nameof(AddDoctorPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public async void Get()
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

    public void Clear()
    {
        Doctors = new ObservableCollection<DoctorModel>();
    }
}
