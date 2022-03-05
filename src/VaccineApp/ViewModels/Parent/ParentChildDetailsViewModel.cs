using Core.Models;

using DAL.Persistence;

using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Parent;


public class ParentChildDetailsViewModel : ViewModelBase
{
    private ChildModel _child;
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<VaccineModel> _vaccines;
    public ParentChildDetailsViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void GetQueryProperty(ChildModel child)
    {
        _child = child;
    }

    public ChildModel Child
    {
        get { return _child; }
        set { _child = value; OnPropertyChanged(); }
    }

    public async void GetVaccines()
    {
        try
        {
            Vaccines = await _unitOfWork.GetVaccines(Child.Id.ToString());
        }
        catch (Exception)
        {
            return;
        }
    }

    public IEnumerable<VaccineModel> Vaccines
    {
        get { return _vaccines; }
        set { _vaccines = value; OnPropertyChanged(); }
    }
}
