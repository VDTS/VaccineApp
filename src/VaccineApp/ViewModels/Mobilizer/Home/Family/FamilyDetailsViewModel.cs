using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Family.Child;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;

public class FamilyDetailsViewModel : ViewModelBase
{
    private FamilyModel _family;
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<ChildModel> _childs;

    public FamilyDetailsViewModel(UnitOfWork unitOfWork)
    {
        AddChildCommand = new Command(AddChild);

        Childs = new ObservableCollection<ChildModel>();
        _unitOfWork = unitOfWork;
    }

    public async void Get()
    {
        try
        {
            Childs = await _unitOfWork.GetChilds(Family.Id.ToString());
        }
        catch (Exception)
        {
            return;
        }
    }

    private async void AddChild()
    {
        var route = $"{nameof(AddChildPage)}?FamilyId={Family.Id.ToString()}";
        await Shell.Current.GoToAsync(route);
    }

    public void GetQueryProperty(FamilyModel family)
    {
        Family = family;
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

    public ICommand AddChildCommand { private set; get; }

}
