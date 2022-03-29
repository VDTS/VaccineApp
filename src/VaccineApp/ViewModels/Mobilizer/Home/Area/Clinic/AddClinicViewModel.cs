using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

public partial class AddClinicViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;

    [ObservableProperty]
    ClinicModel _clinic;

    [ObservableProperty]
    bool _isLocationAvailable;

    ClinicValidator _clinicValidator { get; set; }


    public AddClinicViewModel(UnitOfWork unitOfWork, ClinicModel clinic, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _clinic = clinic;
        _clinicValidator = new();
    }

    [ICommand]
    async void Post()
    {
        var validationResult = _clinicValidator.Validate(Clinic);
        if (validationResult.IsValid)
        {
            if (IsLocationAvailable)
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    if (location != null)
                    {
                        Clinic.Latitude = location.Latitude.ToString();
                        Clinic.Longitude = location.Longitude.ToString();
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
            var result = await _unitOfWork.AddClinic(Clinic);
            await Shell.Current.GoToAsync("../");
            _toast.MakeToast($"{result.ClinicName} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }
}
