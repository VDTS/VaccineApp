using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family.Child;
public partial class AddChildViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;

    [ObservableProperty]
    ChildModel _child;

    [ObservableProperty]
    string _familyId;

    ChildValidator _childValidator { get; set; }

    public AddChildViewModel(UnitOfWork unitOfWork, ChildModel child, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _child = child;
        _childValidator = new();
    }

    internal void GetQueryProperty(string familyId)
    {
        _familyId = familyId;
    }

    [ICommand]
    async void Post()
    {
        Child.DOB = DateTime.SpecifyKind(Child.DOB, DateTimeKind.Utc);
        var validationResult = _childValidator.Validate(Child);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddChild(_child, _familyId);
            await Shell.Current.GoToAsync("..");
            _toast.MakeToast($"{result.FullName} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }
}