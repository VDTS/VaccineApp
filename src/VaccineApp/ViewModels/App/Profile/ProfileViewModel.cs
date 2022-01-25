using Auth.Services;
using Core.Models;
using Newtonsoft.Json;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.App.Profile;

namespace VaccineApp.ViewModels.App.Profile;
public class ProfileViewModel : ViewModelBase
{
    private ProfileModel _profile;
    private readonly SignInService _signInService;

    public ProfileViewModel(ProfileModel profile, SignInService signInService)
    {
        _profile = profile;
        _signInService = signInService;
        EditCommand = new Command(Edit);
        Get();
    }

    private async void Edit()
    {
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

    }

    public ProfileModel Profile
    {
        get { return _profile; }
        set { _profile = value; OnPropertyChanged(); }
    }

    public ICommand EditCommand { private set; get; }
}