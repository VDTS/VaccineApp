using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using VaccineApp.Views.Mobilizer.Home.Family;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;
public partial class FamiliesListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfwork;

    [ObservableProperty]
    FamilyModel _selectedFamily;

    [ObservableProperty]
    IEnumerable<FamilyModel> _families;

    public FamiliesListViewModel(UnitOfWork unitOfwork)
    {
        SelectedFamily = new();
        _unitOfwork = unitOfwork;
        Families = new ObservableCollection<FamilyModel>();
    }

    [ICommand]
    async void FamilyDetails()
    {
        if (SelectedFamily != null)
        {
            var JsonSelectedFamily = JsonConvert.SerializeObject(SelectedFamily);
            var route = $"{nameof(FamilyDetailsPage)}?Family={JsonSelectedFamily}";
            await Shell.Current.GoToAsync(route);

            SelectedFamily = null;
        }
    }

    [ICommand]
    async void AddFamily(object obj)
    {
        var route = $"{nameof(AddFamilyPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public async void Get()
    {
        try
        {
            Families = await _unitOfwork.GetFamilies();
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        Families = new ObservableCollection<FamilyModel>();
        SelectedFamily = null;
    }
}
