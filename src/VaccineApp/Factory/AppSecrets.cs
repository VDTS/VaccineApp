namespace VaccineApp.Factory;

public class AppSecrets
{
    public string? FirebaseApiKey { get; set; }
    public string? FirebaseBaseAddress { get; set; }
    public int GithubAppId { get; set; }
    public string? FirebaseStorageAddress { get; set; }


    // For offline env only
    public string? FirebaseBaseAddress_Offline { get; set; }
}