using Core.Models;
using DAL.Persistence;

using Newtonsoft.Json;

using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Area.School;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.School;

public class SchoolsListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<SchoolModel> _schools;
    private SchoolModel _selectedSchool;

    public SchoolsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Schools = new ObservableCollection<SchoolModel>();
        AddSchoolCommand = new Command(AddSchool);
        SchoolDetailsCommand = new Command(SchoolDetails);
    }
    public ICommand AddSchoolCommand { private set; get; }
    public ICommand SchoolDetailsCommand { private set; get; }

    public SchoolModel SelectedSchool
    {
        get { return _selectedSchool; }
        set { _selectedSchool = value; OnPropertyChanged(); }
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
    private async void AddSchool(object obj)
    {
        var route = $"{nameof(AddSchoolPage)}";
        await Shell.Current.GoToAsync(route);
    }

    private async void SchoolDetails()
    {
        if (SelectedSchool == null)
        {
            return;
        }
        else
        {
            var SelectedItemJson = JsonConvert.SerializeObject(SelectedSchool);
            var route = $"{nameof(SchoolDetailsPage)}?School={SelectedItemJson}";
            await Shell.Current.GoToAsync(route);
            SelectedSchool = null;
        }
    }

    public async void Get()
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

    public void Clear()
    {
        Schools = new ObservableCollection<SchoolModel>();
    }
}
