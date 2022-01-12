using VaccineApp.ViewModels.Access.SignIn;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;
using VaccineApp.Views.Access.SignIn;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;

namespace VaccineApp.Factory;

public static class AddServiceExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection Services)
    {

        Services.AddScoped<MasjeedListViewModel>();
        Services.AddScoped<SignInViewModel>();

        return Services;
    }

    public static IServiceCollection AddViewPages(this IServiceCollection services)
    {
        services.AddScoped<MasjeedsListPage>();
        services.AddScoped<SignInPage>();

        return services;
    }
}