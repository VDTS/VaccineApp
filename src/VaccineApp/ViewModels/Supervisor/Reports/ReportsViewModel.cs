using Core.CountsPerParentModels;
using VaccineApp.Features;
using Core.Models;
using DAL.Persistence;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.Supervisor.Reports;

public class ReportsViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IToast _toast;
    private ClusterModel _cluster;
    private IEnumerable<TeamModel> _teams;
    private ObservableCollection<AnonymousChildrenCountPerTeamModel> _anonymouChildrenCountGroupByTeam;
    public ReportsViewModel(UnitOfWork unitOfWork, IToast toast)
    {
        NonResedentialChildrenReportCommand = new Command(NonResedentialChildrenReport);
        Cluster = new ClusterModel();
        Teams = new ObservableCollection<TeamModel>();
        AnonymouChildrenCountGroupByTeam = new ObservableCollection<AnonymousChildrenCountPerTeamModel>();
        _unitOfWork = unitOfWork;
        _toast = toast;
    }

    private async void NonResedentialChildrenReport()
    {
        await GetAnonymousChildren();
        PdfDocument document = new();
        PdfPage page = document.Pages.Add();

        PdfFont firstHeaderFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);
        PdfFont secondHeaderFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
        PdfFont bodyFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 11);

        int x = 0;
        int y = 0;

        string headerString = $"Non Resedential Children Vaccine Report - {DateTime.Now}";
        page.Graphics.DrawString(headerString, firstHeaderFont, PdfBrushes.DarkBlue, new Syncfusion.Drawing.PointF(x, y));
        y += 20;

        string clusterString = $"{Cluster.ClusterName} Cluster";
        page.Graphics.DrawString(clusterString, firstHeaderFont, PdfBrushes.DarkBlue, new Syncfusion.Drawing.PointF(x, y));
        y += 30;

        PdfPen pen = new(PdfBrushes.Black);
        page.Graphics.DrawLine(pen, new Syncfusion.Drawing.PointF(x, y), new Syncfusion.Drawing.PointF(x + 400, y));
        y += 30;

        foreach (var item in AnonymouChildrenCountGroupByTeam)
        {
            string teamName = $"{item.TeamNo} Team";
            page.Graphics.DrawString(teamName, secondHeaderFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(x, y));
            y += 30;

            foreach (var value in item.ChildrenByType)
            {
                x += 15;
                string str = $"{value.ChildType} : {value.Count}";

                page.Graphics.DrawString(str, bodyFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(x, y));

                x -= 15;
                y += 15;
            }
            y += 30;
        }

        MemoryStream stream = new();
        document.Save(stream);

        document.Close(true);

        stream.Position = 0;

        SavePDF($"{nameof(NonResedentialChildrenReport)}.pdf", stream);
    }

    public ICommand NonResedentialChildrenReportCommand { private set; get; }

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
    private void SavePDF(string fileName, Stream data)
    {
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(documentsPath, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        using Stream outStream = File.OpenWrite(filePath);
        data.CopyTo(outStream);

        _toast.MakeToast("PDF report downloaded", filePath);
    }
}
