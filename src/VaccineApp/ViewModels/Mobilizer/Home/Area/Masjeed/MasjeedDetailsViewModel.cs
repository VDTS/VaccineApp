using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

public partial class MasjeedDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    MasjeedModel _masjeed;

    public void GetQueryProperty(MasjeedModel masjeed)
    {
        _masjeed = masjeed;
    }


    [ICommand]
    async void GoToMap()
    {
        if (Masjeed.Latitude != null && Masjeed.Longitude != null)
        {
            var location = new Location(Convert.ToDouble(Masjeed.Latitude), Convert.ToDouble(Masjeed.Longitude));
            var options = new MapLaunchOptions
            {
                Name = null,
                NavigationMode = NavigationMode.Walking
            };
            await Map.OpenAsync(location, options);
        }
    }
}
