using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Family;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;
public class FamiliesListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private FamilyModel _selectedFamily;
    private IEnumerable<FamilyModel> _families;
    public FamiliesListViewModel(UnitOfWork unitOfwork)
    {
        SelectedFamily = new();
        _unitOfwork = unitOfwork;
        Families = new ObservableCollection<FamilyModel>();

        AddFamilyCommand = new Command(AddFamily);
        FamilyDetailsCommand = new Command(FamilyDetails);
        Get();
    }

    private async void FamilyDetails()
    {
        if (SelectedFamily != null)
        {
            var JsonSelectedFamily = JsonConvert.SerializeObject(SelectedFamily);
            var route = $"{nameof(FamilyDetailsPage)}?Family={JsonSelectedFamily}";
            await Shell.Current.GoToAsync(route);

            SelectedFamily = null;
        }
    }

    private async void AddFamily(object obj)
    {
        var route = $"{nameof(AddFamilyPage)}";
        await Shell.Current.GoToAsync(route);
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

    public FamilyModel SelectedFamily
    {
        get { return _selectedFamily; }
        set { _selectedFamily = value; OnPropertyChanged(); }
    }
    public ICommand AddFamilyCommand { private set; get; }
    public ICommand FamilyDetailsCommand { private set; get; }
}
