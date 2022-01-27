using Microsoft.Extensions.Options;
using System.Windows.Input;
using VaccineApp.AppConfigs;

namespace VaccineApp.ViewModels.App.Settings;

public class SettingsViewModel
{
    private readonly IOptions<SettingsDefaultsValues> _defaultSettings;

    public SettingsViewModel(IOptions<SettingsDefaultsValues> defaultSettings)
    {
        ResetAppSettignsDefaultCommand = new Command(ResetAppSettignsDefault);
        _defaultSettings = defaultSettings;
    }

    private void ResetAppSettignsDefault(object obj)
    {

        ResetFontSize();

    }

    private void ResetFontSize()
    {
        Preferences.Set("FontSize", Convert.ToInt32(_defaultSettings.Value.FontSize));
        Application.Current.Resources["defaultFontSize"] = Convert.ToInt32(Preferences.Get("FontSize", 14));
    }

    public ICommand ResetAppSettignsDefaultCommand { private set; get; }
}
