using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Supervisor;

public class StatsViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private ClusterModel _cluster;
    private IEnumerable<TeamModel> _teams;
    private ObservableCollection<FamiliesCountGroupByTeams> _families;
    private int _childs;

    public StatsViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Cluster = new();
        Teams = new ObservableCollection<TeamModel>();
        Families = new ObservableCollection<FamiliesCountGroupByTeams>();
    }
    public void Clear()
    {
        Cluster = new();
        Teams = new ObservableCollection<TeamModel>();
        Families = new ObservableCollection<FamiliesCountGroupByTeams>();
    }
    public async void GetCluster()
    {
        try
        {
            var clusters = await _unitOfWork.GetClusters();
            Cluster = clusters.Where(x => x.Id.ToString() == Preferences.Get("ClusterId", "AnonymousCluster"))
                    .FirstOrDefault();

            GetTeams();
        }
        catch (Exception)
        {
            return;
        }
    }

    private async void GetTeams()
    {
        try
        {
            Teams = await _unitOfWork.GetTeams(Cluster.Id.ToString());

            GetFamilies();
        }
        catch (Exception)
        {
            return;
        }
    }

    private async void GetFamilies()
    {
        try
        {
            foreach (var item in Teams)
            {
                var families = await _unitOfWork.GetFamilies(item.Id.ToString());
                var s = families.ToList().Count;

                Families.Add(new FamiliesCountGroupByTeams() { TeamNo = item.TeamNo.ToString(), FamiliesCount = s });

                foreach (var child in families)
                {
                    Childs += await GetChilds(child.Id.ToString());
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private async Task<int> GetChilds(string id)
    {
        var s = await _unitOfWork.GetChilds(id);
        return s.ToList().Count;
    }

    public ClusterModel Cluster
    {
        get { return _cluster; }
        set { _cluster = value; OnPropertyChanged(); }
    }
    public IEnumerable<TeamModel> Teams
    {
        get { return _teams; }
        set { _teams = value; OnPropertyChanged(); }
    }
    public ObservableCollection<FamiliesCountGroupByTeams> Families
    {
        get { return _families; }
        set { _families = value; OnPropertyChanged(); }
    }
    public int Childs
    {
        get { return _childs; }
        set { _childs = value; OnPropertyChanged(); }
    }
}

public class FamiliesCountGroupByTeams
{
    public string TeamNo { get; set; }
    public int FamiliesCount { get; set; }
}