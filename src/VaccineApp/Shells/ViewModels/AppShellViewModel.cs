using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using VaccineApp.Shells.Views;
using VaccineApp.Views.App.HelpSupport;
using VaccineApp.Views.App.Profile;
using VaccineApp.Views.App.Settings;

namespace VaccineApp.Shells.ViewModels;

public partial class AppShellViewModel : ObservableObject
{
    [ObservableProperty]
    string? userName;

    [ObservableProperty]
    string? role;

    [ObservableProperty]
    string? photoUrl;

    public AppShellViewModel()
    {
        LogoutCommand = new Command(Logout);
        ProfileCommand = new Command(Profile);
        HelpSupportCommand = new Command(HelpSupport);
        SettingsCommand = new Command(Settings);

        UserName = Preferences.Get("DisplayName", null!);
        Role = Preferences.Get("Role", null!);
        PhotoUrl = Preferences.Get("ProfilePicture", null!);
    }

    private void Logout()
    {
        SecureStorage.RemoveAll();
        Preferences.Clear();

        if(Application.Current is not null)
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

    private async void Settings()
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    public ICommand LogoutCommand { private set; get; }
    public ICommand ProfileCommand { private set; get; }
    public ICommand HelpSupportCommand { private set; get; }
    public ICommand SettingsCommand { private set; get; }
}
