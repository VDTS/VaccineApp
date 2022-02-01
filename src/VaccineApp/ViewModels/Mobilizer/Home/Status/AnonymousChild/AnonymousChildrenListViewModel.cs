using Core.GroupByModels;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Status.AnonymousChild;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status.AnonymousChild;

public class AnonymousChildrenListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private IEnumerable<string> _childTypes;
    private ObservableCollection<AnonymousChildrenGroupByChildType> _anonymousChildren;
    public AnonymousChildrenListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        AddChildCommand = new Command(AddChild);
        AnonymousChildren = new ObservableCollection<AnonymousChildrenGroupByChildType>();
        _childTypes = new List<string>()
        {
            "Guest", "IDP", "Refugee", "Return"
        };
    }

    public async void Get()
    {
        try
        {
            var s = await _unitOfWork.GetAnonymousChildren();

            foreach (var item in _childTypes)
            {
                var lp = s.Where(x => x.ChildType == item).ToList();
                AnonymousChildren.Add(new AnonymousChildrenGroupByChildType(item, lp));
            }
        }
        catch (Exception)
        {
            return;
        }
    }
    private async void AddChild(object obj)
    {
        var route = $"{nameof(AddAnonymousChildPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public ICommand AddChildCommand { private set; get; }
    public ObservableCollection<AnonymousChildrenGroupByChildType> AnonymousChildren
    {
        get { return _anonymousChildren; }
        set { _anonymousChildren = value; OnPropertyChanged(); }
    }

    public void Clear()
    {
        AnonymousChildren = new ObservableCollection<AnonymousChildrenGroupByChildType>();
    }
}