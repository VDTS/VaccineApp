using Core.Models;
using DAL.Persistence;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;

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
        SelectedMasjeed = new();

        Get();
    }
    public ICommand PullRefreshCommand { private set; get; }
    public ICommand GoToDetailsPageCommand { private set; get; }
    public ICommand GoToPostPageCommand { private set; get; }

    public IEnumerable<MasjeedModel> Masjeeds
    {
        get { return _masjeeds; }
        set
        {
            _masjeeds = value;
            OnPropertyChanged();
        }
    }
    public MasjeedModel SelectedMasjeed
    {
        get { return _selectedMasjeed; }
        set { _selectedMasjeed = value; OnPropertyChanged(); }
    }
    public bool IsBusy
    {
        get { return _isBusy; }
        set { _isBusy = value; OnPropertyChanged(); }
    }

    private async Task Get()
    {
        Masjeeds = await _unitOfwork.GetMasjeeds();
    }
}