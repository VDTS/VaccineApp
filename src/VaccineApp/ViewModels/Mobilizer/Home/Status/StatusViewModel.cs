using Core.GroupByModels;
using Core.HybridModels;
using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;
using RealCache.Persistence.Migrations;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Status;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status;

public class StatusViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly DbContext<PeriodModel> _dbContext;
    private ObservableCollection<ChildrenGroupByHouseNoModel> _childrenGroupByFamily;
    private ChildWithVaccineStatusModel _selectedChild;
    private string _periodId;
    public StatusViewModel(UnitOfWork unitOfWork, DbContext<PeriodModel> dbContext)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
        ChildrenGroupByFamily = new();
        SelectedChild = new();
        GetPeriod();
        ChildDetailsCommand = new Command(ChildDetails);
    }

    private async void GetPeriod()
    {
        try
        {
            var s = await _unitOfWork.GetActivePeriod();
            _dbContext.CreateDB("mobilizer", "user");
            _dbContext.CreateTable();
            _dbContext.Insert(s);
            _periodId = s.Id.ToString();
        }
        catch (Exception)
        {
            return;
        }
    }

    private async void ChildDetails()
    {
        if (SelectedChild == null)
        {
            return;
        }
        else
        {
            var SelectedItemJson = JsonConvert.SerializeObject(SelectedChild.Child);
            var route = $"{nameof(ChildDetailsPage)}?Child={SelectedItemJson}";
            await Shell.Current.GoToAsync(route);
            SelectedChild = null;
        }
    }

    public async void Get()
    {
        try
        {
            var f = await _unitOfWork.GetFamilies();

            foreach (var item in f)
            {
                var c = await _unitOfWork.GetChilds(item.Id.ToString());
                ObservableCollection<ChildWithVaccineStatusModel> childWithVaccineStatus = new();
                foreach (var child in c)
                {
                    childWithVaccineStatus.Add(await AddStatusToChild(child));
                }

                ChildrenGroupByFamily.Add(new ChildrenGroupByHouseNoModel(item.HouseNo, childWithVaccineStatus.ToList()));
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    public async Task<ChildWithVaccineStatusModel> AddStatusToChild(ChildModel child)
    {
        try
        {
            var s = await _unitOfWork.GetVaccines(child.Id.ToString());
            return new ChildWithVaccineStatusModel
            {
                Child = child,
                VaccineStatus = s.Where(x => x.Period == _periodId).FirstOrDefault().Status
            };
        }
        catch (Exception)
        {
            return new ChildWithVaccineStatusModel
            {
                Child = child,
                VaccineStatus = "🚫"
            };
        }
    }

    public ChildWithVaccineStatusModel SelectedChild
    {
        get { return _selectedChild; }
        set { _selectedChild = value; OnPropertyChanged(); }
    }
    public ObservableCollection<ChildrenGroupByHouseNoModel> ChildrenGroupByFamily
    {
        get { return _childrenGroupByFamily; }
        set { _childrenGroupByFamily = value; OnPropertyChanged(); }
    }
    public ICommand ChildDetailsCommand { private set; get; }
    public void Clear()
    {
        ChildrenGroupByFamily.Clear();
        SelectedChild = null;
    }
}
