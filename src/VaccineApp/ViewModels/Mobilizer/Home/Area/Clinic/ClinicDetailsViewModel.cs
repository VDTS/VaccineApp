using System.Windows.Input;

using Core.Models;

using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;

public class ClinicDetailsViewModel : ViewModelBase
{
    private ClinicModel _clinic;
    public ClinicDetailsViewModel()
    {
        GoToMapCommand = new Command(GoToMap);
    }

    public ICommand GoToMapCommand { private set; get; }
    public void GetQueryProperty(ClinicModel clinic)
    {
        _clinic = clinic;
    }

    public ClinicModel Clinic
    {
        get { return _clinic; }
        set { _clinic = value; OnPropertyChanged(); }
    }

    public async void GoToMap()
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
