using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;

namespace VaccineApp.Factory;

public static class AddServiceExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection Services)
    {

        Services.AddScoped<MasjeedListViewModel>();

        return Services;
    }

    public static IServiceCollection AddViewPages(this IServiceCollection services)
    {
        services.AddScoped<MasjeedsListPage>();

        return services;
    }
}