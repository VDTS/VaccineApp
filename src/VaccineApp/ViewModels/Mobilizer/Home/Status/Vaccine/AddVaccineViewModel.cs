using VaccineApp.Features;
using Core.Models;
using Core.Utility;
using DAL.Persistence;
using RealCache.Persistence.Migrations;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using Core.StaticData;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status.Vaccine;

public class AddVaccineViewModel : ViewModelBase
{
    public string _childId;
    private VaccineModel _vaccine;
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private readonly DbContext<PeriodModel> _dbContext;
    private List<string> _statuses;
    private bool _isLocationAvailable;

    public AddVaccineViewModel(UnitOfWork unitOfWork, IToast toast, DbContext<PeriodModel> dbContext)
    {
        Statuses = VaccineStatus.ListStatuses();
        Vaccine = new();
        PostCommand = new Command(Post);
        _unitOfWork = unitOfWork;
        _toast = toast;
        _dbContext = dbContext;
    }

    private async void Post()
    {
        _dbContext.CreateDB("mobilizer", "user");
        var s = _dbContext.Get();

        try
        {
            DateTimeRange range = new(s.LastOrDefault().StartDate, s.LastOrDefault().EndDate);
            Vaccine.Date = DateTime.SpecifyKind(Vaccine.Date, DateTimeKind.Utc);
            Vaccine.Period = s.LastOrDefault().Id.ToString();
            if (range.IsDateIncludedInRange(Vaccine.Date))
            {
                if (IsLocationAvailable)
                {
                    try
                    {
                        var location = await Geolocation.GetLastKnownLocationAsync();

                        if (location != null)
                        {
                            Vaccine.Latitude = location.Latitude.ToString();
                            Vaccine.Longitude = location.Longitude.ToString();
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }

                await _unitOfWork.AddVaccine(Vaccine, _childId);
                await Shell.Current.GoToAsync("..");
                _toast.MakeToast("Vaccine Added");
            }
            else
            {
                _toast.MakeToast("No current vaccine period.");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception)
        {
            _toast.MakeToast("No current vaccine period.");
            await Shell.Current.GoToAsync("..");
        }
    }

    public void GetQueryProperty(string childId)
    {
        _childId = childId;
    }

    public VaccineModel Vaccine
    {
        get { return _vaccine; }
        set { _vaccine = value; OnPropertyChanged(); }
    }
    public bool IsLocationAvailable
    {
        get { return _isLocationAvailable; }
        set { _isLocationAvailable = value; OnPropertyChanged(); }
    }
    public List<string> Statuses
    {
        get { return _statuses; }
        set { _statuses = value; OnPropertyChanged(); }
    }
    public ICommand PostCommand { private set; get; }
}
