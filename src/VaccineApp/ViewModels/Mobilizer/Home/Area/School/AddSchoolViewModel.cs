using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.School;

public class AddSchoolViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private SchoolModel _school;
    SchoolValidator _schoolValidator { get; set; }


    public AddSchoolViewModel(UnitOfWork unitOfWork, SchoolModel school, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _school = school;
        _schoolValidator = new();

        PostCommand = new Command(Post);
    }

    public ICommand PostCommand { private set; get; }

    private async void Post()
    {
        var validationResult = _schoolValidator.Validate(School);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddSchool(School);
            await Shell.Current.GoToAsync("../");
            _toast.MakeToast($"{result.SchoolName} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }

    public SchoolModel School
    {
        get
        {
            return _school;
        }
        set
        {
            _school = value;
            OnPropertyChanged();
        }
    }
}
