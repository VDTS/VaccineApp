using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.GroupByModels;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.Views.Admin.Home.Team;

namespace VaccineApp.ViewModels.Admin.Home.Team;
public partial class TeamsListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfwork;

    [ObservableProperty]
    ObservableCollection<TeamsGroupByClusterModel> _teamsGroupByCluster;

    public TeamsListViewModel(UnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        TeamsGroupByCluster = new ObservableCollection<TeamsGroupByClusterModel>();
    }

    [ICommand]
    async void AddTeam(object obj)
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
}
