using System.Windows.Input;

using Core.Models;

using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status.Vaccine;

public class VaccineDetailsViewModel : ViewModelBase
{
    public VaccineModel _vaccine { get; set; }
    public VaccineDetailsViewModel()
    {
        GoToMapCommand = new Command(GoToMap);
    }

    public ICommand GoToMapCommand { private set; get; }
    public VaccineModel Vaccine
    {
        get { return _vaccine; }
        set { _vaccine = value; OnPropertyChanged(); }
    }

    public void GetQueryProperty(VaccineModel vaccine)
    {
        _vaccine = vaccine;
    }

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
