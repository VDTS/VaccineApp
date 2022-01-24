using System.Windows.Input;
using VaccineApp.Shells.Views;
using VaccineApp.Views.App.HelpSupport;
using VaccineApp.Views.App.Profile;

namespace VaccineApp.Shells.ViewModels;

public class AppShellViewModel
{
    public AppShellViewModel()
    {
        LogoutCommand = new Command(Logout);
        ProfileCommand = new Command(Profile);
        HelpSupportCommand = new Command(HelpSupport);
    }

    private void Logout()
    {
        SecureStorage.RemoveAll();
        Preferences.Clear();
        Application.Current.MainPage = new Accessshell();
    }

    private async void Profile()
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }

    private async void HelpSupport()
    {
        await Shell.Current.GoToAsync(nameof(HelpSupportPage));
    }

    public ICommand LogoutCommand { private set; get; }
    public ICommand ProfileCommand { private set; get; }
    public ICommand HelpSupportCommand { private set; get; }
}
