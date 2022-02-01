using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Area.Influencer;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;

public class InfluencersListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<InfluencerModel> _influencers;
    public InfluencersListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Influencer = new ObservableCollection<InfluencerModel>();
        AddInfluencerCommand = new Command(AddInfluencer);
    }
    public ICommand AddInfluencerCommand { private set; get; }
    public IEnumerable<InfluencerModel> Influencer
    {
        get { return _influencers; }
        set
        {
            _influencers = value;
            OnPropertyChanged();
        }
    }

    private async void AddInfluencer(object obj)
    {
        var route = $"{nameof(AddInfluencerPage)}";
        await Shell.Current.GoToAsync(route);
    }
    public async void Get()
    {
        try
        {
            Influencer = await _unitOfWork.GetInfluencers();
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        Influencer = new ObservableCollection<InfluencerModel>();
    }
}
