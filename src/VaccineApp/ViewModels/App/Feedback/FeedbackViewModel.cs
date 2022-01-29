using Core.Features;
using Core.Models;
using Microsoft.Extensions.Options;
using Octokit;
using System.Reflection;
using System.Windows.Input;
using VaccineApp.Factory;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.App.Feedback;
public class FeedbackViewModel : ViewModelBase
{
    private FeedbackModel _feedback;
    private readonly IToast _toast;
    private readonly IOptions<AppSettings> _options;
    private bool _isBugChecked;
    private bool _isIdeaChecked;
    private bool _isWindowsChecked;
    private bool _isAndroidChecked;
    private bool _isAllChecked;
    private bool _isEnhancementChecked;
    public FeedbackViewModel(IToast toast, IOptions<AppSettings> options)
    {
        Feedback = new();
        SubmitIssueOnGithubCommand = new Command(SubmitIssue);
        _toast = toast;
        _options = options;
        IsWindowsChecked = true;
        IsAndroidChecked = false;
        IsBugChecked = false;
        IsIdeaChecked = true;
        IsAllChecked = false;
        IsEnhancementChecked = false;
    }

    public ICommand SubmitIssueOnGithubCommand { private set; get; }
    public FeedbackModel Feedback
    {
        get
        {
            return _feedback;
        }
        set
        {
            _feedback = value;
            OnPropertyChanged();
        }
    }
    public bool IsWindowsChecked
    {
        get { return _isWindowsChecked; }
        set { _isWindowsChecked = value; OnPropertyChanged(); }
    }
    public bool IsAndroidChecked
    {
        get { return _isAndroidChecked; }
        set { _isAndroidChecked = value; OnPropertyChanged(); }
    }
    public bool IsBugChecked
    {
        get { return _isBugChecked; }
        set { _isBugChecked = value; OnPropertyChanged(); }
    }
    public bool IsIdeaChecked
    {
        get { return _isIdeaChecked; }
        set { _isIdeaChecked = value; OnPropertyChanged(); }
    }
    public bool IsEnhancementChecked
    {
        get { return _isEnhancementChecked; }
        set { _isEnhancementChecked = value; OnPropertyChanged(); }
    }
    public bool IsAllChecked
    {
        get { return _isAllChecked; }
        set { _isAllChecked = value; OnPropertyChanged(); }
    }

    private async void SubmitIssue()
    {
        var jwtToken = GenerateToken();
        var newIssue = CreateNewIssue();
        var issueNumber = await CreateIssue(jwtToken, newIssue);

        var issueUrl = @$"https://github.com/NaveedAhmadHematmal/VaccineApp/issues/{issueNumber}";

        await Shell.Current.GoToAsync("..");

        _toast.MakeToast($"submitted issue url: {issueUrl}");
    }

    private NewIssue CreateNewIssue()
    {
        NewIssue newIssue = new NewIssue(Feedback.Title)
        {
            Body = Feedback.Body
        };

        if (IsWindowsChecked == true)
        {
            newIssue.Labels.Add("area/desktop 🖥");
        }
        else if (IsAndroidChecked)
        {
            newIssue.Labels.Add("area/android 📱");
        }
        else if (IsAllChecked)
        {
            newIssue.Labels.Add("area/desktop 🖥");
            newIssue.Labels.Add("area/android 📱");
        }

        if (IsIdeaChecked)
        {
            newIssue.Labels.Add("t/idea 💡");
        }
        else if(IsBugChecked)
        {
            newIssue.Labels.Add("t/bug 🪲");
        }
        else if (IsEnhancementChecked)
        {
            newIssue.Labels.Add("t/enhancement ☀️");
        }
        return newIssue;
    }

    private string GenerateToken()
    {
        // This code copies Embededd file to Cache.
        var cacheFile = Path.Combine(FileSystem.CacheDirectory, "vdtsapp.2022-01-27.private-key.pem");
        if (File.Exists(cacheFile))
            File.Delete(cacheFile);
        using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("VaccineApp.SecretFiles.vdtsapp.2022-01-27.private-key.pem"))
        using (var file = new FileStream(cacheFile, System.IO.FileMode.Create, FileAccess.Write))
        {
            resource.CopyTo(file);
        }

        var generator = new GitHubJwt.GitHubJwtFactory(
                new GitHubJwt.FilePrivateKeySource(cacheFile),
                new GitHubJwt.GitHubJwtFactoryOptions
                {
                    AppIntegrationId = _options.Value.GithubAppId,
                    ExpirationSeconds = 600
                });

        return generator.CreateEncodedJwtToken();
    }

    public async Task<string> CreateIssue(string jwt, NewIssue newIssue)
    {
        var appClient = new GitHubClient(new ProductHeaderValue("VaccineApp"))
        {
            Credentials = new Credentials(jwt, AuthenticationType.Bearer)
        };

        var installations = await appClient.GitHubApps.GetAllInstallationsForCurrent();

        var id = installations.FirstOrDefault().Id;

        var response = await appClient.GitHubApps.CreateInstallationToken(id);


        var installationClient = new GitHubClient(new ProductHeaderValue("VaccineApp"))
        {
            Credentials = new Credentials(response.Token)
        };

        var IssueRes = await installationClient.Issue.Create("NaveedAhmadHematmal", "VaccineApp", newIssue);

        return IssueRes.Number.ToString();
    }
}
