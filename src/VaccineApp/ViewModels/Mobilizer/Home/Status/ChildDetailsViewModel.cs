using Core.Models;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Status.Vaccine;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status;

public class ChildDetailsViewModel : ViewModelBase
{
    private ChildModel _child;
    public ChildDetailsViewModel()
    {
    }
    public void GetQueryProperty(ChildModel child)
    {
        Child = child;
        AddVaccineCommand = new Command(AddVaccine);
    }

    private async void AddVaccine()
    {
        var route = $"{nameof(AddVaccinePage)}?ChildId={Child.Id.ToString()}";
        await Shell.Current.GoToAsync(route);
    }

    public void Get()
    {

    }
    public ChildModel Child
    {
        get { return _child; }
        set { _child = value; OnPropertyChanged(); }
    }

    public ICommand AddVaccineCommand { private set; get; }
}
