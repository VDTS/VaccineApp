using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Admin.Home.Team;
public partial class AddTeamViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;
    TeamValidator _teamValidator;

    [ObservableProperty]
    TeamModel _team;

    [ObservableProperty]
    IEnumerable<ClusterModel> _clustersList;

    [ObservableProperty]
    ClusterModel _selectedCluster;

    public AddTeamViewModel(UnitOfWork unitOfWork, TeamModel team, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _team = team;
        _toast = toast;
        SelectedCluster = new();
        ClustersList = new ObservableCollection<ClusterModel>();
        _teamValidator = new();
        GetClusters();
    }

    async void GetClusters()
    {
        try
        {
            ClustersList = await _unitOfWork.GetClusters();
        }
        catch (Exception)
        {
            return;
        }
    }

    [ICommand]
    async void Post()
    {
        var validationResult = _teamValidator.Validate(_team);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddTeam(_team, SelectedCluster.Id.ToString());
            await Shell.Current.GoToAsync("..");
            _toast.MakeToast($"{result.CHWName}'s team has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }
}