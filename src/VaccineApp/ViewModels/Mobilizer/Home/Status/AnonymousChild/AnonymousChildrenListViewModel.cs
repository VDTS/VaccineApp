using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.GroupByModels;
using Core.StaticData;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.Views.Mobilizer.Home.Status.AnonymousChild;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status.AnonymousChild;

public partial class AnonymousChildrenListViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;

    [ObservableProperty]
    IEnumerable<string> _childTypes;

    [ObservableProperty]
    ObservableCollection<AnonymousChildrenGroupByChildType> _anonymousChildren;
    public AnonymousChildrenListViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        AnonymousChildren = new ObservableCollection<AnonymousChildrenGroupByChildType>();
        _childTypes = AnonymousChildTypes.ChildTypes();
    }

    public async void Get()
    {
        try
        {
            var s = await _unitOfWork.GetAnonymousChildren();

            foreach (var item in _childTypes)
            {
                var lp = s.Where(x => x.ChildType == item).ToList();
                if (lp.Count >= 1)
                {
                    AnonymousChildren.Add(new AnonymousChildrenGroupByChildType(item, lp));
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    [ICommand]
    async void AddChild(object obj)
    {
        var route = $"{nameof(AddAnonymousChildPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public void Clear()
    {
        AnonymousChildren = new ObservableCollection<AnonymousChildrenGroupByChildType>();
    }
}