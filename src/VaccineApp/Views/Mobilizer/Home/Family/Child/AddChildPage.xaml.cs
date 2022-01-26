using VaccineApp.ViewModels.Mobilizer.Home.Family.Child;

namespace VaccineApp.Views.Mobilizer.Home.Family.Child;
[QueryProperty(nameof(FamilyId), nameof(FamilyId))]
public partial class AddChildPage : ContentPage
{
    private readonly AddChildViewModel _viewModel;

    public AddChildPage(AddChildViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        _viewModel.GetQueryProperty(FamilyId);
        this.BindingContext = _viewModel;
    }
    public string FamilyId { get; set; }
}