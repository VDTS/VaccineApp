namespace VaccineApp.Views.App.Settings;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Application.Current.Resources["defaultFontSize"] = Convert.ToInt32(Preferences.Get("FontSize", 14));
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        Preferences.Set("FontSize", Convert.ToInt32(e.NewValue));
        Application.Current.Resources["defaultFontSize"] = Convert.ToInt32(Preferences.Get("FontSize", 14));
    }
}