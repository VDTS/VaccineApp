using System.Collections.ObjectModel;

using Core.HybridModels;
using Core.Utility;

using DAL.Persistence;

using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Parent.VaccineNotifications;

public class VaccinesNotificationsViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private string _familyId;
    private ObservableCollection<ChildWithNextVaccineModel> _childWithNextVaccine;

    public VaccinesNotificationsViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        ChildWithNextVaccine = new();
    }

    public async Task GetParentFamily()
    {
        try
        {
            var s = await _unitOfWork.GetParentFamily();
            _familyId = s.Id.ToString();
        }
        catch (Exception)
        {
            return;
        }
    }

    public async Task GetChilds()
    {
        try
        {
            var s = await _unitOfWork.GetChilds(_familyId);

            foreach (var item in s)
            {
                if (true)
                {
                    ChildWithNextVaccine.Add(new ChildWithNextVaccineModel
                    {
                        Child = item,
                        NextVaccine = NextVaccineFinder.FindNextVaccine(item.DOB)
                    });
                }
            }
        }
        catch (Exception)
        {
            return;
        }
        
    }

    public ObservableCollection<ChildWithNextVaccineModel> ChildWithNextVaccine
    {
        get { return _childWithNextVaccine; }
        set { _childWithNextVaccine = value; OnPropertyChanged(); }
    }
}
