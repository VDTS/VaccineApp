using Core.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Family.Child;
public class AddChildViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private ChildModel _child;
    ChildValidator _childValidator { get; set; }


    public AddChildViewModel(UnitOfWork unitOfWork, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _child = new();
        _childValidator = new();

        PostCommand = new Command(Post);
    }

    public ICommand PostCommand { private set; get; }

    private async void Post()
    {
        var validationResult = _childValidator.Validate(Child);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddChild(_child, "FamilyId");
            _toast.MakeToast($"{result.FullName} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }

    public ChildModel Child
    {
        get
        {
            return _child;
        }
        set
        {
            _child = value;
            OnPropertyChanged();
        }
    }
}