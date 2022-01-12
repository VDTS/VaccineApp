using Core.Models;
using Core.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Windows.Input;
using VaccineApp.Factory;
using VaccineApp.ViewModels.Base;

namespace VaccineApp.ViewModels.App.Feedback;
public class FeedbackViewModel : ViewModelBase
{
    private readonly GitHubIssueSubmitService _gitHubIssueSubmitService;
    private readonly IOptions<AppSettings> _options;
    private FeedbackModel _feedback;

    public FeedbackViewModel(GitHubIssueSubmitService gitHubIssueSubmitService, IOptions<AppSettings> options)
    {
        Feedback = new();
        _gitHubIssueSubmitService = gitHubIssueSubmitService;
        _options = options;
        SubmitIssueOnGithubCommand = new Command(SubmitIssue);
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

    private async void SubmitIssue()
    {
        try
        {
            _gitHubIssueSubmitService.ConfigureGithubService("VaccineApp", _options.Value.GithubSecretKey, "Org", "Repo");
            var data = JsonConvert.SerializeObject(Feedback);
            var result = await _gitHubIssueSubmitService.SubmitIssue(data);

            if(result == "OK")
            {
                await Application.Current.MainPage.DisplayAlert("Done", "Issue submitted successfully", "Ok");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
    }
}
