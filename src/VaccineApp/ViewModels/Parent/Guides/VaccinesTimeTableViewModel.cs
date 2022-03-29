using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VaccineApp.ViewModels.Parent.Guides;

public partial class VaccinesTimeTableViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<VaccinesTimeTableModel> _vaccines;

    public VaccinesTimeTableViewModel()
    {
        Vaccines = new ObservableCollection<VaccinesTimeTableModel>();

        Get();
    }


    private void Get()
    {
        Vaccines.Add(new VaccinesTimeTableModel("Immediately after borns", "BCG, OPV 0, Hep B"));
        Vaccines.Add(new VaccinesTimeTableModel("6th week", "Penta 1, OPV 1, PCV 1, Rota 1"));
        Vaccines.Add(new VaccinesTimeTableModel("10th week", "Penta 2, OPV 2, PCV 2, Rota 2"));
        Vaccines.Add(new VaccinesTimeTableModel("14th week", "Penta 3, OPV 3, PCV 3, IPV"));
        Vaccines.Add(new VaccinesTimeTableModel("9th month", "Measles 1, OPV 4"));
        Vaccines.Add(new VaccinesTimeTableModel("18th month", "Measles 2"));
    }
}

public record class VaccinesTimeTableModel(string Age, string PreferredVaccines);