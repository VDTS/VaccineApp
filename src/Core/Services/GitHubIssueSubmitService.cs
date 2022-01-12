using Core.Models;
using Newtonsoft.Json;
using Octokit;
namespace Core.Services;
public class GitHubIssueSubmitService
{
    private string _githubProductHeaderValue { get; set; }
    private string _githubApiKeyForCreatingIssues { get; set; }
    private string _githubRepoOwner { get; set; }
    private string _githubRepoName { get; set; }

    public void ConfigureGithubService(
        string githubProductHeaderValue,
        string githubApiKeyForCreatingIssues,
        string githubRepoOwner,
        string githubRepoName)
    {
        _githubProductHeaderValue = githubProductHeaderValue;
        _githubApiKeyForCreatingIssues = githubApiKeyForCreatingIssues;
        _githubRepoOwner = githubRepoOwner;
        _githubRepoName = githubRepoName;
    }
    public async Task<string> SubmitIssue(string feedback)
    {
        var data = JsonConvert.DeserializeObject<FeedbackModel>(feedback);

        var client = new GitHubClient(new ProductHeaderValue(_githubProductHeaderValue));

        var tokenAuth = new Credentials(_githubApiKeyForCreatingIssues);
        client.Credentials = tokenAuth;

        var i = new NewIssue(data.Title)
        {
            Body = data.Body
        };

        foreach (var item in data.Labels)
        {
            i.Labels.Add(item);
        }


        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            throw new Exception("No internet connection");
        }
        else
        {
            try
            {
                var issue = await client.Issue.Create(_githubRepoOwner, _githubRepoName, i);
                if (issue.State.Value.ToString() == "Open")
                {
                    return "OK";
                }
                else
                {
                    throw new Exception("Issue not submitted");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
