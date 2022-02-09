using Auth.Services;
using Core.Models;
using Newtonsoft.Json;
using System.Windows.Input;
using VaccineApp.Shells.Views;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Access.ForgotPassword;

namespace VaccineApp.ViewModels.Access.SignIn;
public class SignInViewModel : ViewModelBase
{
    private readonly SignInService _signInService;
    private string _userEmailInput;
    private string _userPasswordInput;

    public SignInViewModel(SignInService signInService)
    {
        _signInService = signInService;
        SignInCommand = new Command(SignIn);
        ForgotPasswordCommand = new Command(ForgotPassword);
    }

    private async void ForgotPassword()
    {
        var route = $"{nameof(ForgotPasswordPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public ICommand SignInCommand { private set; get; }
    public ICommand ForgotPasswordCommand { private set; get; }
    public string UserEmailInput
    {
        get { return _userEmailInput; }
        set
        {
            _userEmailInput = value;
            OnPropertyChanged();
        }
    }
    public string UserPasswordInput {
        get { return _userPasswordInput; }
        set
        {
            _userPasswordInput = value;
            OnPropertyChanged();
        }
    }
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
        }
        else if (c.Role == "Supervisor")
        {
            Preferences.Set(nameof(c.ClusterId), c.ClusterId);
        }
        else if (c.Role == "Mobilizer")
        {
            Preferences.Set(nameof(c.ClusterId), c.ClusterId);
            Preferences.Set(nameof(c.TeamId), c.TeamId);
        }
        else if (c.Role == "Parent")
        {
            Preferences.Set(nameof(c.ClusterId), c.ClusterId);
            Preferences.Set(nameof(c.TeamId), c.TeamId);
            Preferences.Set(nameof(c.FamilyId), c.FamilyId);
        }

        return c.Role;
    }
}
