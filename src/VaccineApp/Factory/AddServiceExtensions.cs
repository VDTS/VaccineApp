using VaccineApp.Shells.Views;
using VaccineApp.ViewModels.Access.SignIn;
using VaccineApp.ViewModels.Admin.Home.Cluster;
using VaccineApp.ViewModels.Admin.Home.Team;
using VaccineApp.ViewModels.Admin.Home.User;
using VaccineApp.ViewModels.App.Feedback;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;
using VaccineApp.ViewModels.Mobilizer.Home.Area.School;
using VaccineApp.ViewModels.Mobilizer.Home.Family;
using VaccineApp.ViewModels.Mobilizer.Home.Family.Child;
using VaccineApp.Views.Access.SignIn;
using VaccineApp.Views.Admin.Home.Cluster;
using VaccineApp.Views.Admin.Home.Team;
using VaccineApp.Views.Admin.Home.User;
using VaccineApp.Views.App;
using VaccineApp.Views.Mobilizer.Home.Area.Clinic;
using VaccineApp.Views.Mobilizer.Home.Area.Doctor;
using VaccineApp.Views.Mobilizer.Home.Area.Influencer;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;
using VaccineApp.Views.Mobilizer.Home.Area.School;
using VaccineApp.Views.Mobilizer.Home.Family;
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
        Services.AddScoped<AddClusterViewModel>();
        Services.AddScoped<AddTeamViewModel>();
        Services.AddScoped<ClustersListViewModel>();
        Services.AddScoped<TeamsListViewModel>();
        Services.AddScoped<AddFamilyViewModel>();
        Services.AddScoped<FamiliesListViewModel>();
        Services.AddScoped<AddUserViewModel>();
        Services.AddScoped<ClinicsListViewModel>();
        Services.AddScoped<DoctorsListViewModel>();
        Services.AddScoped<InfluencersListViewModel>();
        Services.AddScoped<SchoolsListViewModel>();

        return Services;
    }
    public static IServiceCollection AddViewPages(this IServiceCollection services)
    {
        services.AddScoped<MasjeedsListPage>();
        services.AddScoped<SignInPage>();
        services.AddScoped<FeedbackPage>();
        services.AddScoped<AddChildPage>();
        services.AddScoped<AddClusterPage>();
        services.AddScoped<AddTeamPage>();
        services.AddScoped<ClustersListPage>();
        services.AddScoped<TeamsListPage>();
        services.AddScoped<AddFamilyPage>();
        services.AddScoped<FamiliesListPage>();
        services.AddScoped<AddUserPage>();
        services.AddScoped<Appshell>();
        services.AddScoped<Accessshell>();
        services.AddScoped<ClinicsListPage>();
        services.AddScoped<DoctorsListPage>();
        services.AddScoped<InfluencersListPage>();
        services.AddScoped<SchoolsListPage>();

        return services;
    }
}