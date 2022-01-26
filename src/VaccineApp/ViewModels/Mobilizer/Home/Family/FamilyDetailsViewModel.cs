using Core.Models;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;

public class FamilyDetailsViewModel : ViewModelBase
{
    private FamilyModel _family;
    public FamilyDetailsViewModel()
    {

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
}
