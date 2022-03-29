using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Admin.Home.Cluster;
public partial class AddClusterViewModel : ObservableObject
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private ClusterValidator _clusterValidator;

    [ObservableProperty]
    ClusterModel _cluster;

    public AddClusterViewModel(UnitOfWork unitOfWork, ClusterModel cluster, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _cluster = cluster;
        _toast = toast;
        _clusterValidator = new();
    }

    [ICommand]
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
}