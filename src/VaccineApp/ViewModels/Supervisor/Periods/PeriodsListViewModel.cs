using System.Windows.Input;
using VaccineApp.Views.Supervisor.Periods;

namespace VaccineApp.ViewModels.Supervisor.Periods;

public class PeriodsListViewModel
{
    public PeriodsListViewModel()
    {
        AddPeriodCommand = new Command(AddPeriod);
    }

    private async void AddPeriod(object obj)
    {
        var route = $"{nameof(AddPeriodPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public ICommand AddPeriodCommand { private set; get; }
}
