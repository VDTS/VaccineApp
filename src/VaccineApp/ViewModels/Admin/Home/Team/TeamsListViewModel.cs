using Core.GroupByModels;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Admin.Home.Team;

namespace VaccineApp.ViewModels.Admin.Home.Team;
public class TeamsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private ObservableCollection<TeamsGroupByClusterModel> _teamsGroupByCluster;

    public TeamsListViewModel(UnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        TeamsGroupByCluster = new ObservableCollection<TeamsGroupByClusterModel>();
        AddTeamCommand = new Command(AddTeam);
    }

    private async void AddTeam(object obj)
    {
        var route = $"{nameof(AddTeamPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public async void Get()
    {
        try
        {
            var f = await _unitOfwork.GetClusters();

            foreach (var item in f)
            {
                var c = await _unitOfwork.GetTeams(item.Id.ToString());

                TeamsGroupByCluster.Add(new TeamsGroupByClusterModel(item.ClusterName, c.ToList()));
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        TeamsGroupByCluster = new ObservableCollection<TeamsGroupByClusterModel>();
    }
    public ObservableCollection<TeamsGroupByClusterModel> TeamsGroupByCluster
    {
        get { return _teamsGroupByCluster; }
        set { _teamsGroupByCluster = value; OnPropertyChanged(); }
    }

    public ICommand AddTeamCommand { private set; get; }
}
