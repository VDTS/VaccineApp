using Core.GroupByModels;
using Core.HybridModels;
using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;
using RealCache.Persistence.Migrations;
using System.Collections.ObjectModel;
using VaccineApp.Views.Mobilizer.Home.Status;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Mobilizer.Home.Status;

public partial class StatusViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly DbContext<PeriodModel> _dbContext;

    [ObservableProperty]
    ObservableCollection<ChildrenGroupByHouseNoModel> _childrenGroupByFamily;

    [ObservableProperty]
    ChildWithVaccineStatusModel _selectedChild;

    [ObservableProperty]
    string _periodId;
    public StatusViewModel(UnitOfWork unitOfWork, DbContext<PeriodModel> dbContext)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
        ChildrenGroupByFamily = new();
        SelectedChild = new();
        GetPeriod();
    }

    async void GetPeriod()
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

    [ICommand]
    async void ChildDetails()
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

    public void Clear()
    {
        ChildrenGroupByFamily.Clear();
        SelectedChild = null;
    }
}
