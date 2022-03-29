using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VaccineApp.ViewModels.Parent;

public partial class ParentChildDetailsViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    ChildModel _child;

    [ObservableProperty]
    IEnumerable<VaccineModel> _vaccines;

    public ParentChildDetailsViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void GetQueryProperty(ChildModel child)
    {
        _child = child;
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
}
