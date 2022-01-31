using Core.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Supervisor.Announcements;

public class AddAnnouncementViewModel : ViewModelBase
{
    private AnnouncementModel _announcement;
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;

    public AddAnnouncementViewModel(UnitOfWork unitOfWork, IToast toast)
    {
        Announcement = new();
        PostCommand = new Command(Post);
        _unitOfWork = unitOfWork;
        _toast = toast;
    }

    private async void Post(object obj)
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

    public AnnouncementModel Announcement
    {
        get { return _announcement; }
        set { _announcement = value; OnPropertyChanged(); }
    }

    public ICommand PostCommand { private set; get; }
}
