using Auth.Services;
using AutoMapper;
using Core.Features;
using Core.Models;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Windows.Input;
using VaccineApp.Factory;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.App.Profile;

public class EditProfileViewModel : ViewModelBase
{
    private ProfileModel _profile;
    private EditProfileModel _editProfile;
    private readonly IMapper _mapper;
    private readonly IOptionsSnapshot<FirebasePrivateKey> _firebasePrivateKey;
    private readonly IToast _toast;
    private AccountService _accountService;
    private EditProfileModelValidator _validationRules;
    public EditProfileViewModel(
        EditProfileModel editProfile,
        ProfileModel profile,
        IMapper mapper,
        IOptionsSnapshot<FirebasePrivateKey> firebasePrivateKey,
        IToast toast,
        AccountService accountService)
    {
        _editProfile = editProfile;
        _mapper = mapper;
        _firebasePrivateKey = firebasePrivateKey;
        _toast = toast;
        _accountService = accountService;
        _profile = profile;
        _validationRules = new();

        ChangeProfileCommand = new Command(ChangeProfile);
        ChangePasswordCommand = new Command(ChangePassword);
    }

    private async void ChangePassword()
    {
        var validationResult = _validationRules.Validate(EditProfile);

        if (validationResult.IsValid)
        {
            try
            {
                _ = _accountService.ChangeAccountPassword(await SecureStorage.GetAsync("IdToken"), EditProfile.Password);
                _toast.MakeToast("Password Changed");

            }
            catch (Exception ex)
            {
                _toast.MakeToast(ex.Message);
            }
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }

    private async void ChangeProfile(object obj)
    {
        var googleSecret = JsonConvert.SerializeObject(_firebasePrivateKey.Value);
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(googleSecret)
            });
        }

        UserRecordArgs args = new UserRecordArgs()
        {
            Email = EditProfile.Email,
            PhoneNumber = $"+{EditProfile.PhoneNumber}",
            DisplayName = EditProfile.DisplayName,
            Uid = EditProfile.LocalId
        };

        UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);

        _toast.MakeToast(userRecord.DisplayName, "Updated");
    }

    public void GetQueryProperty(ProfileModel profile)
    {
        EditProfile = _mapper.Map<EditProfileModel>(profile);
    }

    public EditProfileModel EditProfile
    {
        get { return _editProfile; }
        set { _editProfile = value; OnPropertyChanged(); }
    }
    public ProfileModel Profile
    {
        get { return _profile; }
        set { _profile = value; OnPropertyChanged(); }
    }

    public ICommand ChangeProfileCommand { private set; get; }
    public ICommand ChangePasswordCommand { private set; get; }
}
