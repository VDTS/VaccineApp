namespace VaccineApp.Views.Parent.QR;

public partial class QRGeneratedImagePage : ContentPage
{
	public QRGeneratedImagePage()
	{
		InitializeComponent();

		GetImage();
	}

	public void GetImage()
    {
		// local path	
		var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
		var generatedQrName = "familyGeneratedImage.png";
		var localPath = Path.Combine(documentsPath, generatedQrName);

		QRImage.Source = ImageSource.FromStream(() =>
		{
			var stream = File.OpenRead(localPath);
			return stream;
		});

	}
}
