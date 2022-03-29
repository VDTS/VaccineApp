using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using VaccineApp.AppConfigs;

namespace VaccineApp.ViewModels.App.Settings;

public partial class SettingsViewModel : ObservableObject
{
    readonly IOptions<SettingsDefaultsValues> _defaultSettings;

    public SettingsViewModel(IOptions<SettingsDefaultsValues> defaultSettings)
    {
        _defaultSettings = defaultSettings;
    }

    [ICommand]
    void ResetAppSettignsDefault(object obj)
    {

        ResetFontSize();

    }

    void ResetFontSize()
    {
        Preferences.Set("FontSize", Convert.ToInt32(_defaultSettings.Value.FontSize));
        Application.Current.Resources["defaultFontSize"] = Convert.ToInt32(Preferences.Get("FontSize", 14));
    }
}
