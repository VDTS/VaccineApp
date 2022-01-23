using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Area.School;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.School;

public class SchoolsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<SchoolModel> _schools;
    public SchoolsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Schools = new ObservableCollection<SchoolModel>();
        AddSchoolCommand = new Command(AddSchool);
        Get();
    }
    public ICommand AddSchoolCommand { private set; get; }
    public IEnumerable<SchoolModel> Schools
    {
        get { return _schools; }
        set
        {
            _schools = value;
            OnPropertyChanged();
        }
    }
    private async void AddSchool(object obj)
    {
        var route = $"{nameof(AddSchoolPage)}";
        await Shell.Current.GoToAsync(route);
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
