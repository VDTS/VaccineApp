using System.Windows.Input;
using VaccineApp.Shells.Views;

namespace VaccineApp.Shells.ViewModels;

public class AppShellViewModel
{
    public AppShellViewModel()
    {
        LogoutCommand = new Command(Logout);
    }

    private void Logout()
    {
        SecureStorage.RemoveAll();
        Preferences.Clear();
        Application.Current.MainPage = new Accessshell();
    }

    public ICommand LogoutCommand { private set; get; }
}
