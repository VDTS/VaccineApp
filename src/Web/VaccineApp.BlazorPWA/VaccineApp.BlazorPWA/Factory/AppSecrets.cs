namespace VaccineApp.BlazorPWA.Factory;

public class AppSecrets
{
    public string FirebaseApiKey { get; set; }
    public string FirebaseBaseAddress { get; set; }

    // For offline env only
    public string FirebaseBaseAddress_Offline { get; set; }
}