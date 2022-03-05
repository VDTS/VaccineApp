using Auth.Services;

using Core.Models;

using DAL.Persistence;

using Newtonsoft.Json;

using System.Windows.Input;

using VaccineApp.ViewModels.Base;
using VaccineApp.Views.App.Profile;

namespace VaccineApp.ViewModels.App.Profile;
public class ProfileViewModel : ViewModelBase
{
    private ProfileModel _profile;
    private readonly SignInService _signInService;
    private readonly UnitOfWork _unitOfWork;
    private string _clusterName;
    private string _teamName;
    private string _familyName;

    public ProfileViewModel(ProfileModel profile, SignInService signInService, UnitOfWork unitOfWork)
    {
        _profile = profile;
        _signInService = signInService;
        _unitOfWork = unitOfWork;
        EditCommand = new Command(Edit);
        Get();
    }

    private async void Edit()
    {
        // The below line remove PhotoUrl because Route Query can't route urls
        Profile.PhotoUrl = "profiledefaultimage.png";
        var result = JsonConvert.SerializeObject(Profile);
        var route = $"{nameof(EditProfilePage)}?Profile={result}";
        await Shell.Current.GoToAsync(route);
    }

    private async void Get()
    {
        var idToken = await SecureStorage.GetAsync("IdToken");
        var i = await _signInService.AccountInfoLookup(idToken);
        var s = JsonConvert.DeserializeObject<AccountInfoLookupModel>(i);
        var c = JsonConvert.DeserializeObject<CustomAttributes>(s.users[0].customAttributes);

        string profileImage;

        if (s.users[0].providerUserInfo[1].photoUrl == null)
        {
            profileImage = "profiledefaultimage.png";
        }
        else
        {
            profileImage = s.users[0].providerUserInfo[1].photoUrl;
        }

        Profile = new()
        {
            LocalId = s.users[0].localId,
            DisplayName = s.users[0].displayName,
            Role = c.Role,
            PhoneNumber = s.users[0].phoneNumber,
            Email = s.users[0].email,
            ClusterId = c.ClusterId,
            TeamId = c.TeamId,
            FamilyId = c.FamilyId,
            PhotoUrl = profileImage
        };


        await LoadClaims();
    }

    public async Task LoadClaims()
    {
        if (Profile.Role == "Supervisor")
        {
            await LoadCluster();
        }
        else if (Profile.Role == "Mobilizer")
        {
            await LoadCluster();
            await LoadTeam();
        }
        else if (Profile.Role == "Parent")
        {
            await LoadCluster();
            await LoadTeam();
            await LoadFamily();
        }
    }

    public async Task LoadCluster()
    {
        try
        {
            var s = await _unitOfWork.GetClusters();
            ClusterName = s.Where(x => x.Id.ToString() == Profile.ClusterId).FirstOrDefault().ClusterName;
        }
        catch (Exception)
        {
            return;
        }
    }
    public async Task LoadTeam()
    {
        try
        {
            var s = await _unitOfWork.GetTeams(Profile.ClusterId);
            TeamName = s.Where(x => x.Id.ToString() == Profile.TeamId).FirstOrDefault().TeamNo;
        }
        catch (Exception)
        {
            return;
        }
    }
    public async Task LoadFamily()
    {
        try
        {
            var s = await _unitOfWork.GetFamilies(Profile.TeamId);
            FamilyName = s.Where(x => x.Id.ToString() == Profile.FamilyId).FirstOrDefault().ParentName;
        }
        catch (Exception)
        {
            return;
        }
    }

    public ProfileModel Profile
    {
        get { return _profile; }
        set { _profile = value; OnPropertyChanged(); }
    }

    public string ClusterName
    {
        get { return _clusterName; }
        set { _clusterName = value; OnPropertyChanged(); }
    }
    public string TeamName
    {
        get { return _teamName; }
        set { _teamName = value; OnPropertyChanged(); }
    }
    public string FamilyName
    {
        get { return _familyName; }
        set { _familyName = value; OnPropertyChanged(); }
    }
    public ICommand EditCommand { private set; get; }
}