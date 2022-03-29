using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

public partial class AddMasjeedViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;
    MasjeedValidator _clinicValidator { get; set; }

    [ObservableProperty]
    MasjeedModel _masjeed;

    [ObservableProperty]
    bool _isLocationAvailable;

    public AddMasjeedViewModel(UnitOfWork unitOfWork, MasjeedModel masjeed, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _masjeed = masjeed;
        _clinicValidator = new();
    }

    [ICommand]
    async void Post()
    {
        var validationResult = _clinicValidator.Validate(Masjeed);
        if (validationResult.IsValid)
        {
            if (IsLocationAvailable)
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    if (location != null)
                    {
                        Masjeed.Latitude = location.Latitude.ToString();
                        Masjeed.Longitude = location.Longitude.ToString();
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }

            var result = await _unitOfWork.AddMasjeed(Masjeed);
            await Shell.Current.GoToAsync("../");
            _toast.MakeToast($"{result.MasjeedName} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }
}
