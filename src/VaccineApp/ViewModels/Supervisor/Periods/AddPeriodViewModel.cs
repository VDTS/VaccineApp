using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Supervisor.Periods;

public class AddPeriodViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private PeriodModel _period;
    public AddPeriodViewModel(UnitOfWork unitOfWork, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        Period = new();
        PostCommand = new Command(Post);
    }

    private async void Post(object obj)
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

    public PeriodModel Period
    {
        get { return _period; }
        set { _period = value; OnPropertyChanged(); }
    }

    public ICommand PostCommand { private set; get; }
}
