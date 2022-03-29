using CommunityToolkit.Mvvm.ComponentModel;
using Core.CountsPerParentModels;
using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;

namespace VaccineApp.ViewModels.Supervisor.Charts;

public partial class ChartsViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    ClusterModel _cluster;

    [ObservableProperty]
    IEnumerable<TeamModel> _teams;

    [ObservableProperty]
    ObservableCollection<FamiliesCountPerTeamsModel> _families;

    [ObservableProperty]
    int _childs;

    public ChartsViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Cluster = new();
        Teams = new ObservableCollection<TeamModel>();
        Families = new ObservableCollection<FamiliesCountPerTeamsModel>();
    }
    public void Clear()
    {
        Cluster = new();
        Teams = new ObservableCollection<TeamModel>();
        Families = new ObservableCollection<FamiliesCountPerTeamsModel>();
    }
    public async Task GetCluster()
    {
        try
        {
            var clusters = await _unitOfWork.GetClusters();
            Cluster = clusters.Where(x => x.Id.ToString() == Preferences.Get("ClusterId", "AnonymousCluster"))
                    .FirstOrDefault();
        }
        catch (Exception)
        {
            return;
        }
    }

    public async Task GetTeams()
    {
        try
        {
            Teams = await _unitOfWork.GetTeams(Cluster.Id.ToString());
        }
        catch (Exception)
        {
            return;
        }
    }

    public async Task GetFamilies()
    {
        try
        {
            foreach (var item in Teams)
            {
                var families = await _unitOfWork.GetFamilies(item.Id.ToString());
                var s = families.ToList().Count;

                Families.Add(new FamiliesCountPerTeamsModel() { TeamNo = item.TeamNo.ToString(), FamiliesCount = s });

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

    async Task<int> GetChilds(string id)
    {
        var s = await _unitOfWork.GetChilds(id);
        return s.ToList().Count;
    }

}