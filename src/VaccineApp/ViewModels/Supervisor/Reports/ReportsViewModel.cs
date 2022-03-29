using Core.CountsPerParentModels;
using Core.HybridModels;
using Core.Models;
using Core.StaticData;
using DAL.Persistence;
using RealCache.Persistence.Migrations;
using System.Collections.ObjectModel;
using VaccineApp.Features;
using VaccineApp.PDFGenerator;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.Supervisor.Reports;

public partial class ReportsViewModel : ObservableObject
{
    readonly UnitOfWork _unitOfWork;
    readonly IToast _toast;
    readonly ReportsGenerator _reportsGenerator;
    readonly DbContext<PeriodModel> _dbContext;

    [ObservableProperty]
    ClusterModel _cluster;

    [ObservableProperty]
    IEnumerable<TeamModel> _teams;

    [ObservableProperty]
    ObservableCollection<AnonymousChildrenCountPerTeamModel> _anonymouChildrenCountGroupByTeam;
    
    public ReportsViewModel(UnitOfWork unitOfWork, IToast toast, ReportsGenerator reportsGenerator, DbContext<PeriodModel> dbContext)
    {
        Cluster = new ClusterModel();
        Teams = new ObservableCollection<TeamModel>();
        AnonymouChildrenCountGroupByTeam = new ObservableCollection<AnonymousChildrenCountPerTeamModel>();
        _unitOfWork = unitOfWork;
        _toast = toast;
        _reportsGenerator = reportsGenerator;
        _dbContext = dbContext;
    }

    [ICommand]
    async void GenerateVaccinePeriodReport()
    {
        await GetChildrenCountPerVaccineStatusPerTeam();
        _reportsGenerator.VaccinePeriodReport(Cluster.ClusterName, childrenCountPerVaccineStatusPerTeam);
    }

    List<ChildrenCountPerVaccineStatusPerTeam> childrenCountPerVaccineStatusPerTeam;

   
    async Task GetChildrenCountPerVaccineStatusPerTeam()
    {
        _dbContext.CreateDB("mobilizer", "user");
        var s = _dbContext.Get();

        childrenCountPerVaccineStatusPerTeam = new();
        List<ChildrenWithVaccineStatus> childrenWithVaccineStatus = new();

        try
        {
            foreach (var item in Teams)
            {
                // Get Families of all teams
                try
                {


                    var f = await _unitOfWork.GetFamilies(item.Id.ToString());

                    foreach (var family in f)
                    {
                        try
                        {


                            // Get children of every family
                            var child = await _unitOfWork.GetChilds(family.Id.ToString());

                            foreach (var ch in child)
                            {
                                try
                                {
                                    var childVaccine = await _unitOfWork.GetVaccines(ch.Id.ToString());
                                    childrenWithVaccineStatus.Add(new ChildrenWithVaccineStatus { Children = ch, VaccineStatus = childVaccine.Where(x => x.Period.ToString() == s.LastOrDefault().Id.ToString()).FirstOrDefault().Status });
                                }
                                catch (Exception)
                                {
                                    return;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                    return;
                }

                var ccpvs = new List<ChildrenCountPerVaccineStatus>();
                foreach (var vs in VaccineStatus.ListStatuses())
                {
                    try
                    {
                        var vsCount = childrenWithVaccineStatus.Where(x => x.VaccineStatus == vs).ToList().Count;
                        ccpvs.Add(new ChildrenCountPerVaccineStatus() { ChildrenCount = vsCount, VaccineStatus = vs });
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                childrenCountPerVaccineStatusPerTeam.Add(new ChildrenCountPerVaccineStatusPerTeam(item.TeamNo, ccpvs));
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    [ICommand]
    async void GenerateNonResedentialChildrenReport()
    {
        await GetAnonymousChildren();
        _reportsGenerator.NonResedentialChildrenReport(Cluster.ClusterName, AnonymouChildrenCountGroupByTeam);
    }
    async Task GetAnonymousChildren()
    {
        try
        {
            foreach (var item in Teams)
            {
                var s = await _unitOfWork.GetAnonymousChildren(item.Id.ToString());
                ChildrenGroupByType refugee = new() { ChildType = "Refugee", Count = s.Where(x => x.ChildType == "Refugee").Count() };
                ChildrenGroupByType guest = new() { ChildType = "Guest", Count = s.Where(x => x.ChildType == "Guest").Count() };
                ChildrenGroupByType IDP = new() { ChildType = "IDP", Count = s.Where(x => x.ChildType == "IDP").Count() };
                ChildrenGroupByType returne = new() { ChildType = "Return", Count = s.Where(x => x.ChildType == "Return").Count() };

                List<ChildrenGroupByType> childrenGroupByTypes = new() { refugee, guest, IDP, returne };
                AnonymouChildrenCountGroupByTeam.Add(new AnonymousChildrenCountPerTeamModel { TeamNo = item.TeamNo, ChildrenByType = childrenGroupByTypes });
            }
        }
        catch (Exception)
        {
            return;
        }
    }
    public async Task GetClusters()
    {
        try
        {
            var s = await _unitOfWork.GetClusters();
            Cluster = s.Where(x => x.Id.ToString() == Preferences.Get("ClusterId", "AnonymousId")).FirstOrDefault();
        }
        catch (Exception)
        {
            return;
        }
    }
    public async Task GetTeams()
    {
        try
        {
            Teams = await _unitOfWork.GetTeams(Cluster.Id.ToString());
        }
        catch (Exception)
        {
            return;
        }
    }
}
