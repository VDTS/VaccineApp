using Auth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using VaccineApp.Features;

namespace VaccineApp.ViewModels.Access.ForgotPassword;

public partial class ForgotPasswordViewModel : ObservableObject
{
    [ObservableProperty]
    string? _email;

    private readonly AccountService _accountService;
    private readonly IToast _toast;

    public ForgotPasswordViewModel(AccountService accountService, IToast toast)
    {
        _accountService = accountService;
        _toast = toast;
    }

    [ICommand]
    private async void ResetPasswordByEmail()
    {
        try
        {
            if (Email is null)
                return; 

            _ = _accountService.SendPasswordResetCode(Email);
            await Shell.Current.GoToAsync("..");
            _toast.MakeToast("Password Changed");
        }
        catch (Exception ex)
        {
            _toast.MakeToast(ex.Message);
        }
    }
}
