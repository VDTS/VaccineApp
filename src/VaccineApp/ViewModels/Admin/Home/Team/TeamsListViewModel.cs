using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Admin.Home.Team;
public class TeamsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private IEnumerable<TeamModel> _teams;

    public TeamsListViewModel(UnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        Teams = new ObservableCollection<TeamModel>();
    }

    public async void Get()
    {
        try
        {
            Teams = await _unitOfwork.GetAllTeams();
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        Teams = new ObservableCollection<TeamModel>();
    }
    public IEnumerable<TeamModel> Teams
    {
        get { return _teams; }
        set { _teams = value; OnPropertyChanged(); }
    }
}
