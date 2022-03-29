using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.Views.Mobilizer.Home.Family.Child;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;

public partial class FamilyDetailsViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    FamilyModel _family;

    [ObservableProperty]
    IEnumerable<ChildModel> _childs;

    public FamilyDetailsViewModel(UnitOfWork unitOfWork)
    {
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

    [ICommand]
    async void AddChild()
    {
        var route = $"{nameof(AddChildPage)}?FamilyId={Family.Id.ToString()}";
        await Shell.Current.GoToAsync(route);
    }

    public void GetQueryProperty(FamilyModel family)
    {
        _family = family;
    }
}
