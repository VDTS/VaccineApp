using VaccineApp.ViewModels.Mobilizer.Home.Family;

namespace VaccineApp.Views.Mobilizer.Home.Family;
public partial class FamiliesListPage : ContentPage
{
    public FamiliesListPage(FamiliesListViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}