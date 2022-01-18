using Core.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Admin.Home.Team;
public class AddTeamViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private TeamModel _team;
    private readonly IToast _toast;
    private TeamValidator _teamValidator;

    public AddTeamViewModel(UnitOfWork unitOfWork, TeamModel team, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _team = team;
        _toast = toast;

        _teamValidator = new();
        PostCommand = new Command(Post);
    }

    private async void Post()
    {
        var validationResult = _teamValidator.Validate(_team);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddTeam(_team);
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
}
