using Auth.Services;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Models;
using Firebase.Storage;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Options;
using VaccineApp.Factory;
using VaccineApp.Features;

namespace VaccineApp.ViewModels.App.Profile;

public partial class EditProfileViewModel : ObservableObject
{
    readonly IOptionsSnapshot<AppSecrets> _options;
    readonly IMapper _mapper;
    readonly IToast _toast;

    [ObservableProperty]
    ProfileModel _profile;

    [ObservableProperty]
    EditProfileModel _editProfile;

    [ObservableProperty]
    AccountService _accountService;

    EditProfileModelValidator _validationRules;
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
    }

    async Task PickPhoto()
    {
        var photo = await MediaPicker.PickPhotoAsync();

        if (photo != null)
        {
            await ChangePhoto(photo);
        }
    }

    async Task CapturePhoto()
    {
        var photo = await MediaPicker.CapturePhotoAsync();

        if (photo != null)
        {
            await ChangePhoto(photo);
        }
    }

    async Task ChangePhoto(FileResult photo)
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

    [ICommand]
    async void PhotoPickingMenu()
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

    [ICommand]
    async void ChangePassword()
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

    [ICommand]
    async void ChangeProfile(object obj)
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
}
