using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status.Vaccine;

public partial class VaccineDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    VaccineModel _vaccine;

    public void GetQueryProperty(VaccineModel vaccine)
    {
        _vaccine = vaccine;
    }

    [ICommand]
    public async void GoToMap()
    {
        if (Vaccine.Latitude != null && Vaccine.Longitude != null)
        {
            var location = new Location(Convert.ToDouble(Vaccine.Latitude), Convert.ToDouble(Vaccine.Longitude));
            var options = new MapLaunchOptions
            {
                Name = null,
                NavigationMode = NavigationMode.Walking
            };
            await Map.OpenAsync(location, options);
        }
    }
}
