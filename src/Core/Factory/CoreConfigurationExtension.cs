using Core.Services;

namespace Core.Factory;
public static class CoreConfigurationExtension
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection service)
    {
        service.AddSingleton<GitHubIssueSubmitService>();
        
        return service;
    }
}