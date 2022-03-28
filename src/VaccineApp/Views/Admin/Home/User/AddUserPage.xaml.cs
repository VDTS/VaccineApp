using VaccineApp.ViewModels.Admin.Home.User;

namespace VaccineApp.Views.Admin.User;
public partial class AddUserPage : ContentPage
{
    public AddUserPage(AddUserViewModel viewModel)
    {
        InitializeComponent();

        ClusterPicker.IsVisible = false;
        TeamPicker.IsVisible = false;
        FamilyPicker.IsVisible = false;

        this.BindingContext = viewModel;
    }

    private void Click_Role(object sender, EventArgs e)
    {
        if (RolePicker.SelectedItem.ToString() == "Admin")
        {
            ClusterPicker.IsVisible = false;
            TeamPicker.IsVisible = false;
            FamilyPicker.IsVisible = false;
        }
        else if (RolePicker.SelectedItem.ToString() == "Supervisor")
        {
            ClusterPicker.IsVisible = true;
            TeamPicker.IsVisible = false;
            FamilyPicker.IsVisible = false;
        }
        else if (RolePicker.SelectedItem.ToString() == "Mobilizer")
        {
            ClusterPicker.IsVisible = true;
            TeamPicker.IsVisible = true;
            FamilyPicker.IsVisible = false;
        }
        else if (RolePicker.SelectedItem.ToString() == "Parent")
        {
            ClusterPicker.IsVisible = true;
            TeamPicker.IsVisible = true;
            FamilyPicker.IsVisible = true;
        }
        else
        {
            ClusterPicker.IsVisible = false;
            TeamPicker.IsVisible = false;
            FamilyPicker.IsVisible = false;
        }
    }
}