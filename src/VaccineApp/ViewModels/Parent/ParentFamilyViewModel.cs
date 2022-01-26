using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Parent;

public class ParentFamilyViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private FamilyModel _family;
    private Guid _familyId;
    private IEnumerable<ChildModel> _childs;

    public ParentFamilyViewModel(UnitOfWork unitOfwork, FamilyModel family)
    {
        _unitOfwork = unitOfwork;
        _family = family;
        _familyId = Guid.Parse(Preferences.Get("FamilyId", "AnonymousFamilyId"));
    }

    public FamilyModel Family
    {
        get { return _family; }
        set { _family = value; OnPropertyChanged(); }
    }

    public IEnumerable<ChildModel> Childs
    {
        get { return _childs; }
        set { _childs = value; OnPropertyChanged(); }
    }

    public async void GetFamily()
    {
        try
        {
            var s = await _unitOfwork.GetFamilies();
            Family = s.Where(x => x.Id == _familyId).FirstOrDefault();
            await GetChilds();
        }
        catch (Exception)
        {
            return;
        }
    }

    public async Task GetChilds()
    {
        Childs = new ObservableCollection<ChildModel>();

        try
        {
            Childs = await _unitOfwork.GetChilds(Family.Id.ToString());
        }
        catch (Exception)
        {
            return;
        }
    }
}
