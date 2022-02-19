using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family;
public class AddFamilyViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private FamilyModel _family;
    private readonly IToast _toast;
    private FamilyValidator _familyValidator;

    public AddFamilyViewModel(UnitOfWork unitOfWork, FamilyModel family, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _family = family;
        _toast = toast;
        _familyValidator = new();

        PostCommand = new Command(Post);
    }

    public ICommand PostCommand { private set; get; }

    private async void Post()
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

    public FamilyModel Family
    {
        get { return _family; }
        set { _family = value; OnPropertyChanged(); }
    }

}
