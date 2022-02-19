using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Admin.Home.Cluster;
public class AddClusterViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private ClusterModel _cluster;
    private readonly IToast _toast;
    private ClusterValidator _clusterValidator;

    public AddClusterViewModel(UnitOfWork unitOfWork, ClusterModel cluster, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _cluster = cluster;
        _toast = toast;
        _clusterValidator = new();

        PostCommand = new Command(Post);
    }

    private async void Post(object obj)
    {
        var validationResult = _clusterValidator.Validate(_cluster);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddCluster(_cluster);
            await Shell.Current.GoToAsync("..");
            _toast.MakeToast($"{result.ClusterName} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }

    public ICommand PostCommand { private set; get; }

    public ClusterModel Cluster
    {
        get { return _cluster; }
        set { _cluster = value; OnPropertyChanged(); }
    }
}