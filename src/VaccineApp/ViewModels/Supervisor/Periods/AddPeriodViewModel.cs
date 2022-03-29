using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Supervisor.Periods;

public partial class AddPeriodViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;

    [ObservableProperty]
    PeriodModel _period;

    public AddPeriodViewModel(UnitOfWork unitOfWork, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        Period = new();
    }

    [ICommand]
    async void Post(object obj)
    {
        try
        {
            await _unitOfWork.AddPeriod(Period);
            await Shell.Current.GoToAsync("..");
            _toast.MakeToast("Period updated");
        }
        catch (Exception)
        {
            return;
        }
    }

}
