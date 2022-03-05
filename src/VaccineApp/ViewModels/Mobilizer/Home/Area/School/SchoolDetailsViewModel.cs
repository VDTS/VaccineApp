using System.Windows.Input;

using Core.Models;

using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.School;

public class SchoolDetailsViewModel : ViewModelBase
{
    public SchoolModel _school { get; set; }
    public SchoolDetailsViewModel()
    {

    }

    public SchoolModel School
    {
        get { return _school; }
        set { _school = value; OnPropertyChanged(); }
    }
    public void GetQueryProperty(SchoolModel school)
    {
        _school = school;
        GoToMapCommand = new Command(GoToMap);
    }

    public ICommand GoToMapCommand { private set; get; }

    public async void GoToMap()
    {
        if (School.Latitude != null && School.Longitude != null)
        {
            var location = new Location(Convert.ToDouble(School.Latitude), Convert.ToDouble(School.Longitude));
            var options = new MapLaunchOptions
            {
                Name = null,
                NavigationMode = NavigationMode.Walking
            };
            await Map.OpenAsync(location, options);
        }
    }
}
