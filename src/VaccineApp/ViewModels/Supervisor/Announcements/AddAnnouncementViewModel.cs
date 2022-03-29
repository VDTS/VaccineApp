using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Supervisor.Announcements;

public partial class AddAnnouncementViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;

    [ObservableProperty]
    AnnouncementModel _announcement;

    public AddAnnouncementViewModel(UnitOfWork unitOfWork, IToast toast)
    {
        Announcement = new();
        _unitOfWork = unitOfWork;
        _toast = toast;
    }

    [ICommand]
    async void Post(object obj)
    {
        try
        {
            var result = await _unitOfWork.AddAnnouncement(Announcement);
            await Shell.Current.GoToAsync("..");
            _toast.MakeToast("Announcement posted");
        }
        catch (Exception)
        {
            return;
        }
    }

}
