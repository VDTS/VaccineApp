using VaccineApp.ViewModels.App.Feedback;

namespace VaccineApp.Views.App;
public partial class FeedbackPage : ContentPage
{
    public FeedbackPage(FeedbackViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}