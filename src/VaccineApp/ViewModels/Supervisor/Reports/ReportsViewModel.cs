using Core.CountsPerParentModels;
using Core.HybridModels;
using Core.Models;
using Core.StaticData;
using DAL.Persistence;
using RealCache.Persistence.Migrations;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.Features;
using VaccineApp.PDFGenerator;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Supervisor.Reports;

public class ReportsViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private readonly ReportsGenerator _reportsGenerator;
    private readonly DbContext<PeriodModel> _dbContext;
    private ClusterModel _cluster;
    private IEnumerable<TeamModel> _teams;
    private ObservableCollection<AnonymousChildrenCountPerTeamModel> _anonymouChildrenCountGroupByTeam;
    public ReportsViewModel(UnitOfWork unitOfWork, IToast toast, ReportsGenerator reportsGenerator, DbContext<PeriodModel> dbContext)
    {
        NonResedentialChildrenReportCommand = new Command(GenerateNonResedentialChildrenReport);
        VaccinePeriodReportCommand = new Command(GenerateVaccinePeriodReport);
        Cluster = new ClusterModel();
        Teams = new ObservableCollection<TeamModel>();
        AnonymouChildrenCountGroupByTeam = new ObservableCollection<AnonymousChildrenCountPerTeamModel>();
        _unitOfWork = unitOfWork;
        _toast = toast;
        _reportsGenerator = reportsGenerator;
        _dbContext = dbContext;
    }

    private async void GenerateVaccinePeriodReport()
    {
        await GetChildrenCountPerVaccineStatusPerTeam();
        _reportsGenerator.VaccinePeriodReport(Cluster.ClusterName, childrenCountPerVaccineStatusPerTeam);
    }

    private List<ChildrenCountPerVaccineStatusPerTeam> childrenCountPerVaccineStatusPerTeam;
    private async Task GetChildrenCountPerVaccineStatusPerTeam()
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

    private async void GenerateNonResedentialChildrenReport()
    {
        await GetAnonymousChildren();
        _reportsGenerator.NonResedentialChildrenReport(Cluster.ClusterName, AnonymouChildrenCountGroupByTeam);
    }
    public ICommand NonResedentialChildrenReportCommand { private set; get; }
    public ICommand VaccinePeriodReportCommand { private set; get; }

    public ClusterModel Cluster
    {
        get { return _cluster; }
        set { _cluster = value; OnPropertyChanged(); }
    }
    public ObservableCollection<AnonymousChildrenCountPerTeamModel> AnonymouChildrenCountGroupByTeam
    {
        get { return _anonymouChildrenCountGroupByTeam; }
        set { _anonymouChildrenCountGroupByTeam = value; OnPropertyChanged(); }
    }

    private async Task GetAnonymousChildren()
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
    public IEnumerable<TeamModel> Teams
    {
        get { return _teams; }
        set { _teams = value; OnPropertyChanged(); }
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
