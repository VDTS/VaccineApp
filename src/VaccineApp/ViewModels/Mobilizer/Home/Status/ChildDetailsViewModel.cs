using Core.Models;
using DAL.Persistence;

using Newtonsoft.Json;

using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Status.Vaccine;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status;

public class ChildDetailsViewModel : ViewModelBase
{
    private ChildModel _child;
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<VaccineModel> _vaccines;
    private VaccineModel _selectedVaccine;

    public ChildDetailsViewModel(UnitOfWork unitOfWork)
    {
        Vaccines = new ObservableCollection<VaccineModel>();
        _unitOfWork = unitOfWork;
    }
    public void GetQueryProperty(ChildModel child)
    {
        Child = child;
        AddVaccineCommand = new Command(AddVaccine);
        VaccineDetailsCommand = new Command(VaccineDetails);
    }

    private async void AddVaccine()
    {
        var route = $"{nameof(AddVaccinePage)}?ChildId={Child.Id.ToString()}";
        await Shell.Current.GoToAsync(route);
    }

    private async void VaccineDetails()
    {
        if (SelectedVaccine == null)
        {
            return;
        }
        else
        {
            var SelectedItemJson = JsonConvert.SerializeObject(SelectedVaccine);
            var route = $"{nameof(VaccineDetailsPage)}?Vaccine={SelectedItemJson}";
            await Shell.Current.GoToAsync(route);
            SelectedVaccine = null;
        }
    }

    public async void GetVaccines()
    {
        try
        {
            Vaccines = await _unitOfWork.GetVaccines(Child.Id.ToString());
        }
        catch (Exception)
        {
            return;
        }
    }

    public VaccineModel SelectedVaccine
    {
        get { return _selectedVaccine; }
        set { _selectedVaccine = value; OnPropertyChanged(); }
    }
    public ChildModel Child
    {
        get { return _child; }
        set { _child = value; OnPropertyChanged(); }
    }
    public IEnumerable<VaccineModel> Vaccines
    {
        get { return _vaccines; }
        set { _vaccines = value; OnPropertyChanged(); }
    }

    public ICommand AddVaccineCommand { private set; get; }
    public ICommand VaccineDetailsCommand { private set; get; }
}
