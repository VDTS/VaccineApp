using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using Core.StaticData;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status.AnonymousChild;

public partial class AddAnonymousChildViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;

    [ObservableProperty]
    AnonymousChildModel _anonymousChild;

    [ObservableProperty]
    string _childType;

    [ObservableProperty]
    List<string> _childTypes;

    public AddAnonymousChildViewModel(UnitOfWork unitOfWork, IToast toast)
    {
        ChildTypes = AnonymousChildTypes.ChildTypes();
        _unitOfWork = unitOfWork;
        _toast = toast;
        AnonymousChild = new();
    }

    [ICommand]
    async void Post()
    {
        try
        {
            await _unitOfWork.AddAnonymousChild(AnonymousChild);
            _toast.MakeToast("Child added");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception)
        {
            return;
        }
    }
}
