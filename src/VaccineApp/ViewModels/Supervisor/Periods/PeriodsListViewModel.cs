using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Supervisor.Periods;

namespace VaccineApp.ViewModels.Supervisor.Periods;

public class PeriodsListViewModel : ViewModelBase
{
    private IEnumerable<PeriodModel> _periods;
    private readonly UnitOfWork _unitOfWork;

    public PeriodsListViewModel(UnitOfWork unitOfWork)
    {
        AddPeriodCommand = new Command(AddPeriod);
        Periods = new ObservableCollection<PeriodModel>();
        _unitOfWork = unitOfWork;
    }

    private async void AddPeriod(object obj)
    {
        var route = $"{nameof(AddPeriodPage)}";
        await Shell.Current.GoToAsync(route);
    }
    public void Clear()
    {
        Periods = new ObservableCollection<PeriodModel>();
    }
    public async void Get()
    {
        try
        {
            Periods = await _unitOfWork.GetPeriods();
        }
        catch (Exception)
        {
            return;
        }
    }
    public IEnumerable<PeriodModel> Periods
    {
        get { return _periods; }
        set { _periods = value; OnPropertyChanged(); }
    }
    public ICommand AddPeriodCommand { private set; get; }
}
