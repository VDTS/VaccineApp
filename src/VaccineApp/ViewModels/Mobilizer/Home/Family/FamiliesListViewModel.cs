using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Family;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;
public class FamiliesListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private IEnumerable<FamilyModel> _families;
    public FamiliesListViewModel(UnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        Families = new ObservableCollection<FamilyModel>();

        AddFamilyCommand = new Command(AddFamily);
        Get();
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

    public ICommand AddFamilyCommand { private set; get; }
}
