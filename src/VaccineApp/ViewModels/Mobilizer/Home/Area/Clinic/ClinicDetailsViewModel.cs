using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

public partial class ClinicDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    ClinicModel _clinic;

    public void GetQueryProperty(ClinicModel clinic)
    {
        _clinic = clinic;
    }

    [ICommand]
    async void GoToMap()
    {
        if (Clinic.Latitude != null && Clinic.Longitude != null)
        {
            var location = new Location(Convert.ToDouble(Clinic.Latitude), Convert.ToDouble(Clinic.Longitude));
            var options = new MapLaunchOptions
            {
                Name = null,
                NavigationMode = NavigationMode.Walking
            };
            await Map.OpenAsync(location, options);
        }
    }
}
