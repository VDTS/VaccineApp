using Auth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;
using VaccineApp.Shells.Views;
using VaccineApp.Views.Access.ForgotPassword;

namespace VaccineApp.ViewModels.Access.SignIn;
public partial class SignInViewModel : ObservableObject
{
    readonly SignInService _signInService;
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    string? _userEmailInput;

    [ObservableProperty]
    string? _userPasswordInput;

    public SignInViewModel(SignInService signInService, UnitOfWork unitOfWork)
    {
        _signInService = signInService;
        _unitOfWork = unitOfWork;
    }

    [ICommand]
    async void ForgotPassword()
    {
        var route = $"{nameof(ForgotPasswordPage)}";
        await Shell.Current.GoToAsync(route);
    }

    [ICommand]
    async void SignIn()
    {
        try
        {
            if (UserEmailInput is not null && UserPasswordInput is not null)
            {
                var s = await _signInService.SignIn(UserEmailInput, UserPasswordInput);

                var idToken = StoreTokens(s);

                var i = await _signInService.AccountInfoLookup(idToken);

                var role = StoreClaims(i);

                Application.Current!.MainPage = new Appshell(role);
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", ex.Message, "Ok");
        }
    }

    string StoreTokens(string json)
    {
        var s = JsonConvert.DeserializeObject<SecureTokensModel>(json);

        if (s is null || s.IdToken is null || s.RefreshToken is null)
            throw new Exception("Can't fetch tokens");

        SecureStorage.SetAsync(nameof(s.IdToken), s.IdToken);
        SecureStorage.SetAsync(nameof(s.RefreshToken), s.RefreshToken);

        return s.IdToken;
    }
    string StoreClaims(string json)
    {
        var s = JsonConvert.DeserializeObject<AccountInfoLookupModel>(json);

#nullable disable
        var c = JsonConvert.DeserializeObject<CustomAttributes>(s.users[0].customAttributes);
#nullable enable
        if (c is null || c.Role is null)
            throw new Exception("Can't fetch user's role");

        Preferences.Set(nameof(c.Role), c.Role);

        if (c.Role == "Admin")
        {
            // No claims to store for admin
            _unitOfWork.AddClaims(null!, null!, null!);
        }
        else if (c.Role == "Supervisor")
        {
            if(c.ClusterId is null)
                throw new Exception("Supervisor should have cluster Id");

            Preferences.Set(nameof(c.ClusterId), c.ClusterId);
            _unitOfWork.AddClaims(c.ClusterId, null!, null!);
        }
        else if (c.Role == "Mobilizer")
        {
            if (c.ClusterId is null || c.TeamId is null)
                throw new Exception("Mobilizer should have cluster and team Ids");

            Preferences.Set(nameof(c.ClusterId), c.ClusterId);
            Preferences.Set(nameof(c.TeamId), c.TeamId);
            _unitOfWork.AddClaims(c.ClusterId, c.TeamId, null!);
        }
        else if (c.Role == "Parent")
        {
            if (c.ClusterId is null || c.TeamId is null || c.FamilyId is null)
                throw new Exception("Parent should have cluster, team and family Ids");

            Preferences.Set(nameof(c.ClusterId), c.ClusterId);
            Preferences.Set(nameof(c.TeamId), c.TeamId);
            Preferences.Set(nameof(c.FamilyId), c.FamilyId);
            _unitOfWork.AddClaims(c.ClusterId, c.TeamId, c.FamilyId);
        }

        return c.Role;
    }
}