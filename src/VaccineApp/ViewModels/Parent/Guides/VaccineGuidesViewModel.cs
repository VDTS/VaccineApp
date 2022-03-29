using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VaccineApp.ViewModels.Parent.Guides;

public partial class VaccineGuidesViewModel : ObservableObject
{
    public string Text;

    [ObservableProperty]
    ObservableCollection<FormattedString> _strings;

    public async Task Get()
    {
        if (string.IsNullOrEmpty(Text))
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("VaccineApp.RawText.VaccineGuides.txt"))
            {
                using var reader = new StreamReader(resource);
                Text = await reader.ReadToEndAsync();
            }
        }


        var list = new List<FormattedString>();
        foreach (var c in Text.Split(" ").ToList().Chunk(100))
        {
            var sb = new StringBuilder();
            foreach (var s in c)
            {
                sb.Append($"{s} ");
            }
            var fs = new FormattedString();
            fs.Spans.Add(new Span()
            {
                Text = sb.ToString(),
                BackgroundColor = Color.FromRgb(250, 250, 249),
                TextColor = Color.FromRgb(0, 0, 0)
            });
            list.Add(fs);
        }

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Strings = new ObservableCollection<FormattedString>(list);
        });
    }
}