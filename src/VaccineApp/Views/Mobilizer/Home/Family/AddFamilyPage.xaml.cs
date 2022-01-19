using VaccineApp.ViewModels.Mobilizer.Home.Family;

namespace VaccineApp.Views.Mobilizer.Home.Family;
public partial class AddFamilyPage : ContentPage
{
    public AddFamilyPage(AddFamilyViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}