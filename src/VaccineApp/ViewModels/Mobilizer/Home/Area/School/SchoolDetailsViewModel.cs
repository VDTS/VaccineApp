using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.School;

public partial class SchoolDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    SchoolModel _school;

    public void GetQueryProperty(SchoolModel school)
    {
        _school = school;
    }

    [ICommand]
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
