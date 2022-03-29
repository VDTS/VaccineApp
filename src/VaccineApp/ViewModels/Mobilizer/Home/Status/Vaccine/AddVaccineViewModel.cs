using VaccineApp.Features;
using Core.Models;
using Core.Utility;
using DAL.Persistence;
using RealCache.Persistence.Migrations;
using Core.StaticData;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status.Vaccine;

public partial class AddVaccineViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;
    readonly DbContext<PeriodModel> _dbContext;

    [ObservableProperty]
    string _childId;

    [ObservableProperty]
    VaccineModel _vaccine;

    [ObservableProperty]
    List<string> _statuses;

    [ObservableProperty]
    bool _isLocationAvailable;

    public AddVaccineViewModel(UnitOfWork unitOfWork, IToast toast, DbContext<PeriodModel> dbContext)
    {
        Statuses = VaccineStatus.ListStatuses();
        Vaccine = new();
        _unitOfWork = unitOfWork;
        _toast = toast;
        _dbContext = dbContext;
    }

    [ICommand]
    async void Post()
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

}
