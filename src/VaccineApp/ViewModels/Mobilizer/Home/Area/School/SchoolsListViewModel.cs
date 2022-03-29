using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;
using DAL.Persistence;

using Newtonsoft.Json;

using System.Collections.ObjectModel;
using VaccineApp.Views.Mobilizer.Home.Area.School;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.School;

public partial class SchoolsListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    IEnumerable<SchoolModel> _schools;

    [ObservableProperty]
    SchoolModel _selectedSchool;

    public SchoolsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Schools = new ObservableCollection<SchoolModel>();
    }

    [ICommand]
    async void AddSchool(object obj)
    {
        var route = $"{nameof(AddSchoolPage)}";
        await Shell.Current.GoToAsync(route);
    }

    [ICommand]
    async void SchoolDetails()
    {
        if (SelectedSchool == null)
        {
            return;
        }
        else
        {
            var SelectedItemJson = JsonConvert.SerializeObject(SelectedSchool);
            var route = $"{nameof(SchoolDetailsPage)}?School={SelectedItemJson}";
            await Shell.Current.GoToAsync(route);
            SelectedSchool = null;
        }
    }

    public async void Get()
    {
        try
        {
            Schools = await _unitOfWork.GetSchools();
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        Schools = new ObservableCollection<SchoolModel>();
    }
}
