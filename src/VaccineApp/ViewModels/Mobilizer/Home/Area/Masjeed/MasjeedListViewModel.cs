using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

public partial class MasjeedListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfwork;

    [ObservableProperty]
    MasjeedModel _selectedMasjeed;

    [ObservableProperty]
    IEnumerable<MasjeedModel> _masjeeds;

    [ObservableProperty]
    bool _isBusy;

    public MasjeedListViewModel(UnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        Masjeeds = new ObservableCollection<MasjeedModel>();
        SelectedMasjeed = new();
    }

    [RelayCommand]
    async void AddMasjeed(object obj)
    {
        var route = $"{nameof(AddMasjeedPage)}";
        await Shell.Current.GoToAsync(route);
    }

    [RelayCommand]
    async void MasjeedDetails()
    {
        if (SelectedMasjeed == null)
        {
            return;
        }
        else
        {
            var SelectedItemJson = JsonConvert.SerializeObject(SelectedMasjeed);
            var route = $"{nameof(MasjeedDetailsPage)}?Masjeed={SelectedItemJson}";
            await Shell.Current.GoToAsync(route);
            SelectedMasjeed = null;
        }
    }
    public async void Get()
    {
        try
        {
            Masjeeds = await _unitOfwork.GetMasjeeds();
        }
        catch (Exception)
        {
            return;
        }
    }
    public void Clear()
    {
        Masjeeds = new ObservableCollection<MasjeedModel>();
    }
}