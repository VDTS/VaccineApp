using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Admin.Home.Cluster;

namespace VaccineApp.ViewModels.Admin.Home.Cluster;
public class ClustersListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private IEnumerable<ClusterModel> _clusters;
    private ClusterModel _selectedCluster;
    public ClustersListViewModel(UnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        Clusters = new ObservableCollection<ClusterModel>();
        AddClusterCommand = new Command(AddCluster);
    }

    private async void AddCluster(object obj)
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

    public IEnumerable<ClusterModel> Clusters
    {
        get { return _clusters; }
        set { _clusters = value; OnPropertyChanged(); }
    }
    public ClusterModel SelectedCluster
    {
        get { return _selectedCluster; }
        set { _selectedCluster = value; OnPropertyChanged(); }
    }

    public ICommand AddClusterCommand { private set; get; }
}
