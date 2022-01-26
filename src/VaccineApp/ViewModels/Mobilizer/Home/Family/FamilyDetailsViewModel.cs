using Core.Models;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Family.Child;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;

public class FamilyDetailsViewModel : ViewModelBase
{
    private FamilyModel _family;
    public FamilyDetailsViewModel()
    {
        AddChildCommand = new Command(AddChild);
    }

    private async void AddChild()
    {
        var route = $"{nameof(AddChildPage)}?FamilyId={_family.Id}";
        await Shell.Current.GoToAsync(route);
    }

    public void GetQueryProperty(FamilyModel family)
    {
        Family = family;
    }

    public FamilyModel Family
    {
        get { return _family; }
        set { _family = value; OnPropertyChanged(); }
    }

    public ICommand AddChildCommand { private set; get; }
}
