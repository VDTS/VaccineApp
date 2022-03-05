using System.Windows.Input;

using Core.Models;

using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

public class MasjeedDetailsViewModel : ViewModelBase
{
    public MasjeedModel _masjeed { get; set; }
    public MasjeedDetailsViewModel()
    {
        GoToMapCommand = new Command(GoToMap);
    }

    public ICommand GoToMapCommand { private set; get; }

    public void GetQueryProperty(MasjeedModel masjeed)
    {
        _masjeed = masjeed;
    }

    public MasjeedModel Masjeed
    {
        get { return _masjeed; }
        set { _masjeed = value; OnPropertyChanged(); }
    }

    public async void GoToMap()
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
