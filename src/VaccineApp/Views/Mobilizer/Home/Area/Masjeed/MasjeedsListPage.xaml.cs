using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

namespace VaccineApp.Views.Mobilizer.Home.Area.Masjeed;
public partial class MasjeedsListPage : ContentPage
{
    public MasjeedsListPage(MasjeedListViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}