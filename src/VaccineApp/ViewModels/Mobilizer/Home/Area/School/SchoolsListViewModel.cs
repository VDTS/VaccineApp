using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.School;

public class SchoolsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<SchoolModel> _schools;
    public SchoolsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Schools = new ObservableCollection<SchoolModel>();

        Get();
    }

    public IEnumerable<SchoolModel> Schools
    {
        get { return _schools; }
        set
        {
            _schools = value;
            OnPropertyChanged();
        }
    }

    private async Task Get()
    {
        try
        {
            Schools = await _unitOfWork.GetSchools();
        }
        catch (Exception)
        {
            return;
        }
    }
}
