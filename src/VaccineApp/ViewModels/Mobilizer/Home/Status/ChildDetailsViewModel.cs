using Core.Models;
using VaccineApp.ViewModels.Base;

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
    }
    public void Get()
    {

    }
    public ChildModel Child
    {
        get { return _child; }
        set { _child = value; OnPropertyChanged(); }
    }
}
