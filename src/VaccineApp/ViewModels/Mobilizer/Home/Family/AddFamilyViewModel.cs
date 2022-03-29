using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;
public partial class AddFamilyViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;
    FamilyValidator _familyValidator;

    [ObservableProperty]
    FamilyModel _family;

    public AddFamilyViewModel(UnitOfWork unitOfWork, FamilyModel family, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _family = family;
        _toast = toast;
        _familyValidator = new();
    }

    [ICommand]
    async void Post()
    {
        var validationResult = _familyValidator.Validate(Family);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddFamily(Family);

            await Shell.Current.GoToAsync("../");
            _toast.MakeToast($"{result.ParentName}'s family has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }
}
