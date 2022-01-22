using Core.Features;
using Core.Models;
using Core.Services;

namespace Core.Factory;
public static class CoreConfigurationExtension
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection service)
    {
        service.AddSingleton<GitHubIssueSubmitService>();

        // Device Features
        service.AddSingleton<IToast, Toaster>();

        // Models
        service.AddSingleton<ClusterModel>();
        service.AddSingleton<TeamModel>();
        service.AddTransient<FamilyModel>();
        service.AddSingleton<ChildModel>();

        return service;
    }
}