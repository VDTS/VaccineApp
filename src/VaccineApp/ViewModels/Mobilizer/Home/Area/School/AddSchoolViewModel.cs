using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.School;

public partial class AddSchoolViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;
    SchoolValidator _schoolValidator { get; set; }

    [ObservableProperty]
    SchoolModel _school;

    [ObservableProperty]
    bool _isLocationAvailable;

    public AddSchoolViewModel(UnitOfWork unitOfWork, SchoolModel school, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _school = school;
        _schoolValidator = new();
    }

    [ICommand]
    async void Post()
    {
        var validationResult = _schoolValidator.Validate(School);
        if (validationResult.IsValid)
        {
            if (IsLocationAvailable)
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    if (location != null)
                    {
                        School.Latitude = location.Latitude.ToString();
                        School.Longitude = location.Longitude.ToString();
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
            var result = await _unitOfWork.AddSchool(School);
            await Shell.Current.GoToAsync("../");
            _toast.MakeToast($"{result.SchoolName} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }
}
