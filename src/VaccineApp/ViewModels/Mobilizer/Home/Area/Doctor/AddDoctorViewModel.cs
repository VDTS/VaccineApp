using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;

public partial class AddDoctorViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;
    DoctorValidator _doctorValidator { get; set; }

    [ObservableProperty]
    DoctorModel _doctor;

    public AddDoctorViewModel(UnitOfWork unitOfWork, DoctorModel doctor, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _doctor = doctor;
        _doctorValidator = new();
    }

    [ICommand]
    async void Post()
    {
        var validationResult = _doctorValidator.Validate(Doctor);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddDoctor(Doctor);
            await Shell.Current.GoToAsync("../");
            _toast.MakeToast($"{result.Name} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }
}
