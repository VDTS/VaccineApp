using Core.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;

public class AddDoctorViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private DoctorModel _doctor;
    DoctorValidator _doctorValidator { get; set; }


    public AddDoctorViewModel(UnitOfWork unitOfWork, DoctorModel doctor, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _doctor = doctor;
        _doctorValidator = new();

        PostCommand = new Command(Post);
    }

    public ICommand PostCommand { private set; get; }

    private async void Post()
    {
        var validationResult = _doctorValidator.Validate(Doctor);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddDoctor(Doctor);
            await Shell.Current.GoToAsync("../");
            _toast.MakeToast($"{result.Name} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }

    public DoctorModel Doctor
    {
        get
        {
            return _doctor;
        }
        set
        {
            _doctor = value;
            OnPropertyChanged();
        }
    }
}
