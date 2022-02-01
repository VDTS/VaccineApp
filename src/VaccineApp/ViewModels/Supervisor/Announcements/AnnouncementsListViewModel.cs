using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Supervisor.Announcements;

namespace VaccineApp.ViewModels.Supervisor.Announcements;

public class AnnouncementsListViewModel : ViewModelBase
{
    private IEnumerable<AnnouncementModel> _announcements;
    private readonly UnitOfWork _unitOfWork;

    public AnnouncementsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Announcements = new ObservableCollection<AnnouncementModel>();
        AddAnnouncementCommand = new Command(AddAnnouncement);
    }

    public async void Get()
    {
        try
        {
            Announcements = await _unitOfWork.GetAnnouncements();
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        Announcements = new ObservableCollection<AnnouncementModel>();
    }

    private async void AddAnnouncement()
    {
        var route = $"{nameof(AddAnnouncementPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public ICommand AddAnnouncementCommand { private set; get; }

    public IEnumerable<AnnouncementModel> Announcements
    {
        get { return _announcements; }
        set { _announcements = value; OnPropertyChanged(); }
    }
}
