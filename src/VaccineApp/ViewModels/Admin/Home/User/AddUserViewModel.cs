using Auth.Services;
using Core.Features;
using Core.Models;
using DAL.Persistence;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Utility.Generators;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Admin.Home.User;
public class AddUserViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private readonly AccountService _accountService;
    private readonly SignInService _signInService;
    private string _email;
    private string _fullName;
    private string _phoneNumber;
    private IList<string> _rolesList;
    private IEnumerable<ClusterModel> _clustersList;
    private IEnumerable<TeamModel> _teamsList;
    private IEnumerable<FamilyModel> _familiesList;
    private string _selectedRole;
    private ClusterModel _selectedCluster;
    private TeamModel _selectedTeam;
    private FamilyModel _selectedFamily;

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
        PostCommand = new Command(Post);
    }
    public ICommand PostCommand { private set; get; }
    public string Email
    {
        get
        {
            return _email;
        }
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }
    public string FullName
    {
        get
        {
            return _fullName;
        }
        set
        {
            _fullName = value;
            OnPropertyChanged();
        }
    }
    public string PhoneNumber
    {
        get
        {
            return _phoneNumber;
        }
        set
        {
            _phoneNumber = value;
            OnPropertyChanged();
        }
    }
    public IList<string> RolesList
    {
        get { return _rolesList; }
        set { _rolesList = value; OnPropertyChanged(); }
    }
    public IEnumerable<ClusterModel> ClustersList
    {
        get { return _clustersList; }
        set { _clustersList = value; OnPropertyChanged(); }
    }
    public IEnumerable<TeamModel> TeamsList
    {
        get { return _teamsList; }
        set { _teamsList = value; OnPropertyChanged(); }
    }
    public IEnumerable<FamilyModel> FamiliesList
    {
        get { return _familiesList; }
        set { _familiesList = value; OnPropertyChanged(); }
    }
    public string SelectedRole
    {
        get { return _selectedRole; }
        set { _selectedRole = value; OnPropertyChanged(); GetClustersData(); }
    }
    public ClusterModel SelectedCluster
    {
        get { return _selectedCluster; }
        set { _selectedCluster = value; OnPropertyChanged(); GetTeamsData(); }
    }
    public TeamModel SelectedTeam
    {
        get { return _selectedTeam; }
        set { _selectedTeam = value; OnPropertyChanged(); GetFamiliesData(); }
    }
    public FamilyModel SelectedFamily
    {
        get { return _selectedFamily; }
        set { _selectedFamily = value; OnPropertyChanged(); }
    }

    private async void GetClustersData()
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
    private async void GetTeamsData()
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
    private async void GetFamiliesData()
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
    private async void Post()
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

    private Dictionary<string, object> AddClaims()
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
