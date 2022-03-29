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
    string _userEmailInput;

    [ObservableProperty]
    string _userPasswordInput;

    public SignInViewModel(SignInService signInService, UnitOfWork unitOfWork)
    {
        _signInService = signInService;
        _unitOfWork = unitOfWork;
    }

    [ICommand]
    private async void ForgotPassword()
    {
        var route = $"{nameof(ForgotPasswordPage)}";
        await Shell.Current.GoToAsync(route);
    }

    [ICommand]
    private async void SignIn()
    {
        try
        {
            var s = await _signInService.SignIn(UserEmailInput, UserPasswordInput);

            var idToken = StoreTokens(s);

            var i = await _signInService.AccountInfoLookup(idToken);

            var role = StoreClaims(i);

            Application.Current.MainPage = new Appshell(role);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
    }

    private string StoreTokens(string json)
    {
        var s = JsonConvert.DeserializeObject<SecureTokensModel>(json);

        SecureStorage.SetAsync(nameof(s.IdToken), s.IdToken);
        SecureStorage.SetAsync(nameof(s.RefreshToken), s.RefreshToken);

        return s.IdToken;
    }
    private string StoreClaims(string json)
    {
        var s = JsonConvert.DeserializeObject<AccountInfoLookupModel>(json);
        var c = JsonConvert.DeserializeObject<CustomAttributes>(s.users[0].customAttributes);

        Preferences.Set(nameof(c.Role), c.Role);

        if (c.Role == "Admin")
        {
            // No claims to store for admin
            _unitOfWork.AddClaims();
        }
        else if (c.Role == "Supervisor")
        {
            Preferences.Set(nameof(c.ClusterId), c.ClusterId);
            _unitOfWork.AddClaims(c.ClusterId);
        }
        else if (c.Role == "Mobilizer")
        {
            Preferences.Set(nameof(c.ClusterId), c.ClusterId);
            Preferences.Set(nameof(c.TeamId), c.TeamId);
            _unitOfWork.AddClaims(c.ClusterId, c.TeamId);
        }
        else if (c.Role == "Parent")
        {
            Preferences.Set(nameof(c.ClusterId), c.ClusterId);
            Preferences.Set(nameof(c.TeamId), c.TeamId);
            Preferences.Set(nameof(c.FamilyId), c.FamilyId);
            _unitOfWork.AddClaims(c.ClusterId, c.TeamId, c.FamilyId);
        }

        return c.Role;
    }
}
