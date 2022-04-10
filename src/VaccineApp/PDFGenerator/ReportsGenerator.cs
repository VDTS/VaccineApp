using Core.CountsPerParentModels;
using Core.HybridModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Collections.ObjectModel;
using VaccineApp.Features;

namespace VaccineApp.PDFGenerator;

public class ReportsGenerator
{
    private readonly IToast _toast;

    public ReportsGenerator(IToast toast)
    {
        _toast = toast;
    }
    public void NonResedentialChildrenReport(
        string clusterName,
        ObservableCollection<AnonymousChildrenCountPerTeamModel> AnonymousChildrenCountPerTeamModel)
    {
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

        string clusterString = $"{clusterName} Cluster";
        page.Graphics.DrawString(clusterString, firstHeaderFont, PdfBrushes.DarkBlue, new Syncfusion.Drawing.PointF(x, y));
        y += 30;

        PdfPen pen = new(PdfBrushes.Black);
        page.Graphics.DrawLine(pen, new Syncfusion.Drawing.PointF(x, y), new Syncfusion.Drawing.PointF(x + 400, y));
        y += 30;

        foreach (var item in AnonymousChildrenCountPerTeamModel)
        {
            string teamName = $"{item.TeamNo} Team";
            page.Graphics.DrawString(teamName, secondHeaderFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(x, y));
            y += 30;

            if(item.ChildrenByType is not null)
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

    public void VaccinePeriodReport(
        string clusterName,
        List<ChildrenCountPerVaccineStatusPerTeam> childrenCountPerVaccineStatus)
    {
        PdfDocument document = new();
        PdfPage page = document.Pages.Add();

        PdfFont firstHeaderFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);
        PdfFont secondHeaderFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
        PdfFont bodyFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 11);

        int x = 0;
        int y = 0;

        string headerString = $"Vaccine Period Report Report - {DateTime.Now}";
        page.Graphics.DrawString(headerString, firstHeaderFont, PdfBrushes.DarkBlue, new Syncfusion.Drawing.PointF(x, y));
        y += 20;

        string clusterString = $"{clusterName} Cluster";
        page.Graphics.DrawString(clusterString, firstHeaderFont, PdfBrushes.DarkBlue, new Syncfusion.Drawing.PointF(x, y));
        y += 30;

        foreach (var item in childrenCountPerVaccineStatus)
        {
            string childPerVaccineStatusPerTeam = $"{item.TeamNo}";
            page.Graphics.DrawString(childPerVaccineStatusPerTeam, secondHeaderFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(x, y));
            y += 30;
            x += 30;

            foreach (var item2 in item)
            {
                string childPerVaccineStatus = $"{item2.VaccineStatus}: {item2.ChildrenCount}";
                page.Graphics.DrawString(childPerVaccineStatus, secondHeaderFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(x, y));
                y += 30;
            }
        }

        MemoryStream stream = new();
        document.Save(stream);

        document.Close(true);

        stream.Position = 0;

        SavePDF($"{nameof(VaccinePeriodReport)}.pdf", stream);
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
