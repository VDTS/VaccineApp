using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;

public class InfluencersListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<InfluencerModel> _influencers;
    public InfluencersListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Influencer = new ObservableCollection<InfluencerModel>();

        Get();
    }

    public IEnumerable<InfluencerModel> Influencer
    {
        get { return _influencers; }
        set
        {
            _influencers = value;
            OnPropertyChanged();
        }
    }


    private async Task Get()
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
}
