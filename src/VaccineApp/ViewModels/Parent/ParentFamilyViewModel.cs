using Core.Models;
using DAL.Persistence;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Parent;

public class ParentFamilyViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private FamilyModel _family;
    private Guid _familyId;
    public ParentFamilyViewModel(UnitOfWork unitOfwork, FamilyModel family)
    {
        _unitOfwork = unitOfwork;
        _family = family;
        _familyId = Guid.Parse(Preferences.Get("FamilyId", "AnonymousFamilyId"));

        GetFamily();
    }

    public FamilyModel Family
    {
        get { return _family; }
        set { _family = value; OnPropertyChanged(); }
    }
    private async void GetFamily()
    {
        try
        {
            var s = await _unitOfwork.GetFamilies();
            Family = s.Where(x => x.Id == _familyId).FirstOrDefault();
        }
        catch (Exception)
        {
            return;
        }
    }

}
