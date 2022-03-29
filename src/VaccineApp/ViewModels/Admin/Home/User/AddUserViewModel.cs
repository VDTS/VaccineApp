using Auth.Services;
using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Utility.Generators;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Admin.Home.User;
public partial class AddUserViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;
    readonly AccountService _accountService;
    readonly SignInService _signInService;

    [ObservableProperty]
    string _email;

    [ObservableProperty]
    string _fullName;

    [ObservableProperty]
    string _phoneNumber;

    [ObservableProperty]
    IList<string> _rolesList;

    [ObservableProperty]
    IEnumerable<ClusterModel> _clustersList;

    [ObservableProperty]
    IEnumerable<TeamModel> _teamsList;

    [ObservableProperty]
    IEnumerable<FamilyModel> _familiesList;

    [ObservableProperty]
    string _selectedRole;

    [ObservableProperty]
    ClusterModel _selectedCluster;

    [ObservableProperty]
    TeamModel _selectedTeam;

    [ObservableProperty]
    FamilyModel _selectedFamily;

    public AddUserViewModel(UnitOfWork unitOfWork, IToast toast, AccountService accountService, SignInService signInService)
    {
        RolesList = new List<string>()
        {
            "Admin", "Supervisor", "Mobilizer", "Parent"
        };

        TeamsList = new ObservableCollection<TeamModel>();
        ClustersList = new ObservableCollection<ClusterModel>();
        FamiliesList = new ObservableCollection<FamilyModel>();
        _unitOfWork = unitOfWork;
        _toast = toast;
        _accountService = accountService;
        _signInService = signInService;
    }

    async void GetClustersData()
    {
        if (SelectedRole == "Supervisor" ||
            SelectedRole == "Mobilizer" ||
            SelectedRole == "Parent")
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
    }
    async void GetTeamsData()
    {
        if (SelectedRole == "Mobilizer" ||
            SelectedRole == "Parent")
        {
            try
            {
                TeamsList = await _unitOfWork.GetTeams(SelectedCluster.Id.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }
    }
    async void GetFamiliesData()
    {
        if (SelectedRole == "Parent")
        {
            try
            {
                FamiliesList = await _unitOfWork.GetFamilies(SelectedTeam.Id.ToString());
            }
            catch (Exception)
            {
                return;
            }
        }
    }

    [ICommand]
    async void Post()
    {
        try
        {
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = Email,
                EmailVerified = false,
                PhoneNumber = $"+{PhoneNumber}",
                Password = PasswordGenerator.GeneratePassword(),
                DisplayName = FullName,
                Disabled = false,
                Uid = Guid.NewGuid().ToString()
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);

            var claims = AddClaims();

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);

            var s = await _signInService.SignIn(args.Email, args.Password);
            var result = JsonConvert.DeserializeObject<SecureTokensModel>(s);
            await _accountService.VerifyEmail(result.IdToken);

            _toast.MakeToast(userRecord.DisplayName, $"Password: {args.Password}");

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            _toast.MakeToast(ex.Message);
        }
    }

    Dictionary<string, object> AddClaims()
    {
        Dictionary<string, object> claims = new();
        if (SelectedRole == "Admin")
        {
            claims.Add("Role", SelectedRole);
        }
        else if (SelectedRole == "Supervisor")
        {
            claims.Add("Role", SelectedRole);
            claims.Add("ClusterId", SelectedCluster.Id.ToString());
        }
        else if (SelectedRole == "Mobilizer")
        {
            claims.Add("Role", SelectedRole);
            claims.Add("ClusterId", SelectedCluster.Id.ToString());
            claims.Add("TeamId", SelectedTeam.Id.ToString());
        }
        else if (SelectedRole == "Parent")
        {
            claims.Add("Role", SelectedRole);
            claims.Add("ClusterId", SelectedCluster.Id.ToString());
            claims.Add("TeamId", SelectedTeam.Id.ToString());
            claims.Add("FamilyId", SelectedFamily.Id.ToString());
        }

        return claims;
    }
}
