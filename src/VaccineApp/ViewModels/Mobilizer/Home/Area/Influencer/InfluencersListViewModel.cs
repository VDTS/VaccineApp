using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.Views.Mobilizer.Home.Area.Influencer;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;

public partial class InfluencersListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    IEnumerable<InfluencerModel> _influencers;

    public InfluencersListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Influencers = new ObservableCollection<InfluencerModel>();
    }

    [ICommand]
    async void AddInfluencer(object obj)
    {
        var route = $"{nameof(AddInfluencerPage)}";
        await Shell.Current.GoToAsync(route);
    }
    public async void Get()
    {
        try
        {
            Influencers = await _unitOfWork.GetInfluencers();
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        Influencers = new ObservableCollection<InfluencerModel>();
    }
}
