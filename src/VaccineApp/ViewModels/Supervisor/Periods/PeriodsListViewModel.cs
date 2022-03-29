using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VaccineApp.Views.Supervisor.Periods;

namespace VaccineApp.ViewModels.Supervisor.Periods;

public partial class PeriodsListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    IEnumerable<PeriodModel> _periods;

    public PeriodsListViewModel(UnitOfWork unitOfWork)
    {
        Periods = new ObservableCollection<PeriodModel>();
        _unitOfWork = unitOfWork;
    }

    [ICommand]
    async void AddPeriod(object obj)
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
}
