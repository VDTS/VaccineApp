using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

public class AddMasjeedViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private MasjeedModel _masjeed;
    MasjeedValidator _clinicValidator { get; set; }


    public AddMasjeedViewModel(UnitOfWork unitOfWork, MasjeedModel masjeed, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _masjeed = masjeed;
        _clinicValidator = new();

        PostCommand = new Command(Post);
    }

    public ICommand PostCommand { private set; get; }

    private async void Post()
    {
        var validationResult = _clinicValidator.Validate(Masjeed);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddMasjeed(Masjeed);
            await Shell.Current.GoToAsync("../");
            _toast.MakeToast($"{result.MasjeedName} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }

    public MasjeedModel Masjeed
    {
        get
        {
            return _masjeed;
        }
        set
        {
            _masjeed = value;
            OnPropertyChanged();
        }
    }
}
