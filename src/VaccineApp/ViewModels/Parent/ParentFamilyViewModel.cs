using Core.HybridModels;
using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;
using SkiaSharp;
using SkiaSharp.QrCode;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Parent.QR;

namespace VaccineApp.ViewModels.Parent;

public class ParentFamilyViewModel : ViewModelBase
{
    private readonly UnitOfWork _unitOfwork;
    private FamilyModel _family;
    private Guid _familyId;
    private IEnumerable<ChildModel> _childs;

    public ParentFamilyViewModel(UnitOfWork unitOfwork, FamilyModel family)
    {
        _unitOfwork = unitOfwork;
        _family = family;
        _familyId = Guid.Parse(Preferences.Get("FamilyId", "AnonymousFamilyId"));
        ShareDataWithQRCodeCommand = new Command(GenerateandGotoThatePage);
    }

    private async void GenerateandGotoThatePage()
    {
        await ShareDataWithQRCode();

        await Shell.Current.GoToAsync(nameof(QRGeneratedImagePage));
    }

    private async Task ShareDataWithQRCode()
    {
        FamilyWithChildrenModel familyWithChildrenModel = new();
        familyWithChildrenModel.Family = Family;
        familyWithChildrenModel.Children = Childs;

        var content = JsonConvert.SerializeObject(familyWithChildrenModel);

        using var generator = new QRCodeGenerator();

        var level = ECCLevel.H;
        var qr = generator.CreateQrCode(content, level);

        var info = new SKImageInfo(512, 512);
        using var surface = SKSurface.Create(info);

        var canvas = surface.Canvas;
        canvas.Render(qr, 512, 512, SKColors.White, SKColors.Black);

        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);

        // local path
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        var generatedQrName = $@"familyGeneratedImage.png";
        var localPath = Path.Combine(documentsPath, generatedQrName);

        using var stream = File.OpenWrite(localPath);
        data.SaveTo(stream);
    }

    public FamilyModel Family
    {
        get { return _family; }
        set { _family = value; OnPropertyChanged(); }
    }

    public IEnumerable<ChildModel> Childs
    {
        get { return _childs; }
        set { _childs = value; OnPropertyChanged(); }
    }

    public async void GetFamily()
    {
        try
        {
            var s = await _unitOfwork.GetFamilies();
            Family = s.Where(x => x.Id == _familyId).FirstOrDefault();
            await GetChilds();
        }
        catch (Exception)
        {
            return;
        }
    }

    public async Task GetChilds()
    {
        Childs = new ObservableCollection<ChildModel>();

        try
        {
            Childs = await _unitOfwork.GetChilds(Family.Id.ToString());
        }
        catch (Exception)
        {
            return;
        }
    }
    public ICommand ShareDataWithQRCodeCommand { private set; get; }
}
