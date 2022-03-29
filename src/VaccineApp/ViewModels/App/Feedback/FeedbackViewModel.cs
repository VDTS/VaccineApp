using VaccineApp.Features;
using Core.Models;
using Microsoft.Extensions.Options;
using Octokit;
using System.Reflection;
using VaccineApp.Factory;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VaccineApp.ViewModels.App.Feedback;
public partial class FeedbackViewModel : ObservableObject
{

    readonly IOptions<AppSecrets> _options;
    readonly IToast _toast;

    [ObservableProperty]
    FeedbackModel _feedback;

    [ObservableProperty]
    bool _isBugChecked;

    [ObservableProperty]
    bool _isIdeaChecked;

    [ObservableProperty]
    bool _isWindowsChecked;

    [ObservableProperty]
    bool _isAndroidChecked;

    [ObservableProperty]
    bool _isAllChecked;

    [ObservableProperty]
    bool _isEnhancementChecked;

    [ObservableProperty]
    bool _isAdminChecked;

    [ObservableProperty]
    bool _isSupervisorChecked;

    [ObservableProperty]
    bool _isMobilizerChecked;

    [ObservableProperty]
    bool _isParentChecked;

    public FeedbackViewModel(IToast toast, IOptions<AppSecrets> options)
    {
        Feedback = new();
        _toast = toast;
        _options = options;
        IsWindowsChecked = true;
        IsAndroidChecked = false;
        IsBugChecked = false;
        IsIdeaChecked = true;
        IsAllChecked = false;
        IsEnhancementChecked = false;
        IsAdminChecked = true;
        IsSupervisorChecked = false;
        IsMobilizerChecked = false;
        IsParentChecked = false;
    }

    [ICommand]
    async void SubmitIssue()
    {
        var jwtToken = GenerateToken();
        var newIssue = CreateNewIssue();
        var issueNumber = await CreateIssue(jwtToken, newIssue);

        var issueUrl = @$"https://github.com/VDTS/VaccineApp/issues/{issueNumber}";

        await Shell.Current.GoToAsync("..");

        _toast.MakeToast($"submitted issue url: {issueUrl}");
    }

    NewIssue CreateNewIssue()
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

        if (IsAdminChecked)
        {
            newIssue.Labels.Add("app/admin 👨‍");
        }
        else if (IsSupervisorChecked)
        {
            newIssue.Labels.Add("app/supervisor 👨");
        }
        else if (IsMobilizerChecked)
        {
            newIssue.Labels.Add("app/mobilizer 👨");
        }
        else if (IsParentChecked)
        {
            newIssue.Labels.Add("app/parent 👨");
        }

        return newIssue;
    }

    string GenerateToken()
    {
        // This code copies Embededd file to Cache.
        var cacheFile = Path.Combine(FileSystem.CacheDirectory, "github_app_private_key.pem");
        if (File.Exists(cacheFile))
            File.Delete(cacheFile);
        using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("VaccineApp.SecretFiles.github_app_private_key.pem"))
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

        var jwtToken = generator.CreateEncodedJwtToken();

        // Removes the Github Private key from Cache.
        if (File.Exists(cacheFile))
            File.Delete(cacheFile);

        return jwtToken;
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

        var IssueRes = await installationClient.Issue.Create("VDTS", "VaccineApp", newIssue);

        return IssueRes.Number.ToString();
    }
}
