using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VaccineApp.Views.Supervisor.Announcements;

namespace VaccineApp.ViewModels.Supervisor.Announcements;

public partial class AnnouncementsListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    IEnumerable<AnnouncementModel> _announcements;

    public AnnouncementsListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Announcements = new ObservableCollection<AnnouncementModel>();
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

    [ICommand]
    private async void AddAnnouncement()
    {
        var route = $"{nameof(AddAnnouncementPage)}";
        await Shell.Current.GoToAsync(route);
    }

}
