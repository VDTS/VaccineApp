using Auth.Services;
using Core.Models;
using Newtonsoft.Json;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.App.Profile;
public class ProfileViewModel : ViewModelBase
{
    private ProfileModel _profile;
    private readonly SignInService _signInService;

    public ProfileViewModel(ProfileModel profile, SignInService signInService)
    {
        _profile = profile;
        _signInService = signInService;

        Get();
    }

    private async void Get()
    {
        var idToken = await SecureStorage.GetAsync("IdToken");
        var i = await _signInService.AccountInfoLookup(idToken);
        var s = JsonConvert.DeserializeObject<AccountInfoLookupModel>(i);
        var c = JsonConvert.DeserializeObject<CustomAttributes>(s.users[0].customAttributes);

        string profileImage;

        if(s.users[0].providerUserInfo[0].photoUrl == null)
        {
            profileImage = "profiledefaultimage.png";
        }
        else
        {
            profileImage = s.users[0].providerUserInfo[0].photoUrl;
        }

        Profile = new()
        {
            DisplayName = s.users[0].displayName,
            Role = c.Role,
            PhoneNumber = s.users[0].phoneNumber,
            Email = s.users[0].email,
            ClusterId = c.ClusterId,
            TeamId = c.TeamId,
            FamilyId = c.FamilyId,
            PhotoUrl = profileImage
        };

    }

    public ProfileModel Profile
    {
        get { return _profile; }
        set { _profile = value; OnPropertyChanged(); }
    }
}