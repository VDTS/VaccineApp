using Auth.Services;
using VaccineApp.Features;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Access.ForgotPassword;

public class ForgotPasswordViewModel : ViewModelBase
{
    private string _email;
    private readonly AccountService _accountService;
    private readonly IToast _toast;

    public ForgotPasswordViewModel(AccountService accountService, IToast toast)
    {
        ResetPasswordByEmailCommand = new Command(ResetPasswordByEmail);
        _accountService = accountService;
        _toast = toast;
    }

    private async void ResetPasswordByEmail()
    {
        try
        {
            _ = _accountService.SendPasswordResetCode(Email);
            await Shell.Current.GoToAsync("..");
            _toast.MakeToast("Password Changed");
        }
        catch (Exception ex)
        {
            _toast.MakeToast(ex.Message);
        }
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; OnPropertyChanged(); }
    }
    public ICommand ResetPasswordByEmailCommand { private set; get; }
}
