using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;

public partial class AddInfluencerViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;

    [ObservableProperty]
    InfluencerModel _influencer;

    InfluencerValidator _influencerValidator { get; set; }

    public AddInfluencerViewModel(UnitOfWork unitOfWork, InfluencerModel influencer, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        _influencer = influencer;
        _influencerValidator = new();
    }

    [ICommand]
    async void Post()
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
}
