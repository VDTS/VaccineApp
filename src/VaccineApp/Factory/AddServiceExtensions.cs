using Core.Models;
using VaccineApp.ViewModels.Access.SignIn;
using VaccineApp.ViewModels.App.Feedback;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;
using VaccineApp.ViewModels.Mobilizer.Home.Family.Child;
using VaccineApp.Views.Access.SignIn;
using VaccineApp.Views.App;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;
using VaccineApp.Views.Mobilizer.Home.Family.Child;

namespace VaccineApp.Factory;

public static class AddServiceExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection Services)
    {

        Services.AddScoped<MasjeedListViewModel>();
        Services.AddScoped<SignInViewModel>();
        Services.AddScoped<FeedbackViewModel>();
        Services.AddScoped<AddChildViewModel>();

        return Services;
    }

    public static IServiceCollection AddModels(this IServiceCollection services)
    {
        services.AddScoped<ChildModel>();

        return services;
    }

    public static IServiceCollection AddViewPages(this IServiceCollection services)
    {
        services.AddScoped<MasjeedsListPage>();
        services.AddScoped<SignInPage>();
        services.AddScoped<FeedbackPage>();
        services.AddScoped<AddChildPage>();

        return services;
    }
}