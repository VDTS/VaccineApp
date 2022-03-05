using Core.Models;
using DAL.Persistence;

using Newtonsoft.Json;

using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;

namespace VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;

public class MasjeedListViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private MasjeedModel _selectedMasjeed;
    private IEnumerable<MasjeedModel> _masjeeds;
    private bool _isBusy;

    public MasjeedListViewModel(UnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        Masjeeds = new ObservableCollection<MasjeedModel>();
        AddMasjeedCommand = new Command(AddMasjeed);
        MasjeedDetailsCommand = new Command(MasjeedDetails);
        SelectedMasjeed = new();
    }
    public ICommand PullRefreshCommand { private set; get; }
    public ICommand MasjeedDetailsCommand { private set; get; }
    public ICommand GoToPostPageCommand { private set; get; }
    public ICommand AddMasjeedCommand { private set; get; }
    private async void AddMasjeed(object obj)
    {
        var route = $"{nameof(AddMasjeedPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public MasjeedModel SelectedMasjeed
    {
        get { return _selectedMasjeed; }
        set { _selectedMasjeed = value; OnPropertyChanged(); }
    }
    public IEnumerable<MasjeedModel> Masjeeds
    {
        get { return _masjeeds; }
        set
        {
            _masjeeds = value;
            OnPropertyChanged();
        }
    }
    public bool IsBusy
    {
        get { return _isBusy; }
        set { _isBusy = value; OnPropertyChanged(); }
    }

    private async void MasjeedDetails()
    {
        if (SelectedMasjeed == null)
        {
            return;
        }
        else
        {
            var SelectedItemJson = JsonConvert.SerializeObject(SelectedMasjeed);
            var route = $"{nameof(MasjeedDetailsPage)}?Masjeed={SelectedItemJson}";
            await Shell.Current.GoToAsync(route);
            SelectedMasjeed = null;
        }
    }
    public async void Get()
    {
        try
        {
            Masjeeds = await _unitOfwork.GetMasjeeds();
        }
        catch (Exception)
        {
            return;
        }
    }

    public void Clear()
    {
        Masjeeds = new ObservableCollection<MasjeedModel>();
    }
}