using Core.GroupByModels;
using DAL.Persistence;
using System.Collections.ObjectModel;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status;

public class StatusViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private ObservableCollection<ChildrenGroupByHouseNoModel> _childrenGroupByFamily;
    public StatusViewModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        ChildrenGroupByFamily = new();
    }

    public async void Get()
    {
        try
        {
            var f = await _unitOfWork.GetFamilies();

            foreach (var item in f)
            {
                var c = await _unitOfWork.GetChilds(item.Id.ToString());

                ChildrenGroupByFamily.Add(new ChildrenGroupByHouseNoModel(item.HouseNo, c.ToList()));
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    public ObservableCollection<ChildrenGroupByHouseNoModel> ChildrenGroupByFamily
    {
        get { return _childrenGroupByFamily; }
        set { _childrenGroupByFamily = value; OnPropertyChanged(); }
    }

    public void Clear()
    {
        ChildrenGroupByFamily.Clear();
    }
}
