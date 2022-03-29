using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.Views.Admin.Home.Cluster;

namespace VaccineApp.ViewModels.Admin.Home.Cluster;
public partial class ClustersListViewModel : ObservableObject
{
    private readonly UnitOfWork _unitOfwork;

    [ObservableProperty]
    IEnumerable<ClusterModel> _clusters;

    [ObservableProperty]
    ClusterModel _selectedCluster;

    public ClustersListViewModel(UnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        Clusters = new ObservableCollection<ClusterModel>();
    }

    [ICommand]
    async void AddCluster(object obj)
    {
        var route = $"{nameof(AddClusterPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public async void Get()
    {
        try
        {
            Clusters = await _unitOfwork.GetClusters();
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        Clusters = new ObservableCollection<ClusterModel>();
    }
}
