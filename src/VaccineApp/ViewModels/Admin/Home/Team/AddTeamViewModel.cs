using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Admin.Home.Team;
public class AddTeamViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private TeamModel _team;
    private readonly IToast _toast;
    private TeamValidator _teamValidator;
    private IEnumerable<ClusterModel> _clustersList;
    private ClusterModel _selectedCluster;

    public AddTeamViewModel(UnitOfWork unitOfWork, TeamModel team, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _team = team;
        _toast = toast;
        SelectedCluster = new();
        ClustersList = new ObservableCollection<ClusterModel>();
        _teamValidator = new();
        GetClusters();
        PostCommand = new Command(Post);
    }

    private async void GetClusters()
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

    private async void Post()
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

    public ICommand PostCommand { private set; get; }

    public TeamModel Team
    {
        get { return _team; }
        set { _team = value; OnPropertyChanged(); }
    }
    public IEnumerable<ClusterModel> ClustersList
    {
        get { return _clustersList; }
        set { _clustersList = value; OnPropertyChanged(); }
    }
    public ClusterModel SelectedCluster
    {
        get { return _selectedCluster; }
        set { _selectedCluster = value; OnPropertyChanged(); }
    }
}
