namespace VaccineApp.Views.App.HelpSupport;

public partial class HelpSupportPage : ContentPage
{
	public HelpSupportPage()
	{
		InitializeComponent();
	}

	public async void Feedback(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(FeedbackPage));
	}
}
