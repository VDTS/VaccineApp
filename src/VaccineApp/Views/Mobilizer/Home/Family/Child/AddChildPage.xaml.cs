using VaccineApp.ViewModels.Mobilizer.Home.Family.Child;

namespace VaccineApp.Views.Mobilizer.Home.Family.Child;
public partial class AddChildPage : ContentPage
{
    public AddChildPage(AddChildViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}