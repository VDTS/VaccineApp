using Core.Features;
using Core.Models;
using DAL.Persistence;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status.AnonymousChild;

public class AddAnonymousChildViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private AnonymousChildModel _anonymousChild;
    private string _childType;
    public AddAnonymousChildViewModel(UnitOfWork unitOfWork, IToast toast)
    {
        _unitOfWork = unitOfWork;
        _toast = toast;
        AnonymousChild = new();
        PostCommand = new Command(Post);
    }

    private async void Post()
    {
        try
        {
            await _unitOfWork.AddAnonymousChild(AnonymousChild);
            _toast.MakeToast("Child added");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception)
        {
            return;
        }
    }

    public AnonymousChildModel AnonymousChild
    {
        get { return _anonymousChild; }
        set { _anonymousChild = value; OnPropertyChanged(); }
    }
    public string ChildType
    {
        get { return _childType; }
        set { _childType = value; OnPropertyChanged(); }
    }

    public ICommand PostCommand { private set; get; }
}
