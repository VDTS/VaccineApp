using Auth.Services;
using Core.Models;
using Newtonsoft.Json;
using System.Windows.Input;
using VaccineApp.Shells.Views;
using VaccineApp.ViewModels.Base;

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
    }

    public ICommand SignInCommand { private set; get; }
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

            StoreTokens(s);

            Application.Current.MainPage = new Appshell();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
    }
    private void StoreTokens(string json)
    {
        var s = JsonConvert.DeserializeObject<SecureTokensModel>(json);

        SecureStorage.SetAsync(nameof(s.IdToken), s.IdToken);
    }
}
