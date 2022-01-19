using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;
public class FamiliesListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private IEnumerable<FamilyModel> _families;
    public FamiliesListViewModel(UnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        Families = new ObservableCollection<FamilyModel>();

        Get();
    }

    private async void Get()
    {
        Families = await _unitOfwork.GetFamilies();
    }

    public IEnumerable<FamilyModel> Families
    {
        get { return _families; }
        set { _families = value; OnPropertyChanged(); }
    }
}
