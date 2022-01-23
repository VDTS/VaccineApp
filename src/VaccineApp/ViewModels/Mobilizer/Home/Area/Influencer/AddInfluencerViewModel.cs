using Core.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;

public class AddInfluencerViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private InfluencerModel _influencer;
    InfluencerValidator _influencerValidator { get; set; }


    public AddInfluencerViewModel(UnitOfWork unitOfWork, InfluencerModel influencer, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _influencer = influencer;
        _influencerValidator = new();

        PostCommand = new Command(Post);
    }

    public ICommand PostCommand { private set; get; }

    private async void Post()
    {
        var validationResult = _influencerValidator.Validate(Influencer);
        if (validationResult.IsValid)
        {
            var result = await _unitOfWork.AddInfluencer(Influencer);
            await Shell.Current.GoToAsync("../");
            _toast.MakeToast($"{result.Name} has been added");
        }
        else
        {
            _toast.MakeToast(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage);
        }
    }

    public InfluencerModel Influencer
    {
        get
        {
            return _influencer;
        }
        set
        {
            _influencer = value;
            OnPropertyChanged();
        }
    }
}
