using Auth.Services;
using AutoMapper;
using Core.Models;
using Firebase.Storage;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Options;
using System.Windows.Input;
using VaccineApp.Factory;
using VaccineApp.Features;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.App.Profile;

public class EditProfileViewModel : ViewModelBase
{
    private ProfileModel _profile;
    private EditProfileModel _editProfile;
    private readonly IMapper _mapper;
    private readonly IToast _toast;
    private AccountService _accountService;
    private readonly IOptionsSnapshot<AppSecrets> _options;
    private EditProfileModelValidator _validationRules;
    public EditProfileViewModel(
        EditProfileModel editProfile,
        ProfileModel profile,
        IMapper mapper,
        IToast toast,
        AccountService accountService,
        IOptionsSnapshot<AppSecrets> options)
    {
        _editProfile = editProfile;
        _mapper = mapper;
        _toast = toast;
        _accountService = accountService;
        _options = options;
        _profile = profile;
        _validationRules = new();

        ChangeProfileCommand = new Command(ChangeProfile);
        ChangePasswordCommand = new Command(ChangePassword);
        ChangePhotoCommand = new Command(PhotoPickingMenu);
    }

    private async void PhotoPickingMenu()
    {
        var action = await Application.Current.MainPage.DisplayActionSheet("Open photo", "Cancel", null, "Gallery", "Camera");

        if (action == "Gallery")
        {
            await PickPhoto();
        }
        else if (action == "Camera")
        {
            await CapturePhoto();
        }
    }

    private async Task PickPhoto()
    {
        var photo = await MediaPicker.PickPhotoAsync();

        if (photo != null)
        {
            await ChangePhoto(photo);
        }
    }

    private async Task CapturePhoto()
    {
        var photo = await MediaPicker.CapturePhotoAsync();

        if (photo != null)
        {
            await ChangePhoto(photo);
        }
    }

    private async Task ChangePhoto(FileResult photo)
    {
        try
        {
            var task = new FirebaseStorage(_options.Value.FirebaseStorageAddress)
                .Child("ProfilePhotos")
                .Child(_editProfile.Email)
                .Child(photo.FileName)
                .PutAsync(await photo.OpenReadAsync());

            var downloadLink = await task;

            UserRecordArgs args = new UserRecordArgs()
            {
                PhotoUrl = downloadLink,
                Uid = EditProfile.LocalId
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);

            _toast.MakeToast(userRecord.DisplayName, "Profile photo updated");
        }
        catch (Exception ex)
        {
            _toast.MakeToast(ex.Message);
        }

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
    public ICommand ChangePhotoCommand { private set; get; }
}
