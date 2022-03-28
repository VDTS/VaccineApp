using VaccineApp.Features;
using VaccineApp.PDFGenerator;
using VaccineApp.ViewModels.Access.ForgotPassword;
using VaccineApp.ViewModels.Access.SignIn;
using VaccineApp.ViewModels.Admin.Home.Cluster;
using VaccineApp.ViewModels.Admin.Home.Team;
using VaccineApp.ViewModels.Admin.Home.User;
using VaccineApp.ViewModels.App.AboutUs;
using VaccineApp.ViewModels.App.Feedback;
using VaccineApp.ViewModels.App.Profile;
using VaccineApp.ViewModels.App.Settings;
using VaccineApp.ViewModels.Mobilizer.Announcements;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;
using VaccineApp.ViewModels.Mobilizer.Home.Area.School;
using VaccineApp.ViewModels.Mobilizer.Home.Family;
using VaccineApp.ViewModels.Mobilizer.Home.Family.Child;
using VaccineApp.ViewModels.Mobilizer.Home.Status;
using VaccineApp.ViewModels.Mobilizer.Home.Status.AnonymousChild;
using VaccineApp.ViewModels.Mobilizer.Home.Status.Vaccine;
using VaccineApp.ViewModels.Parent;
using VaccineApp.ViewModels.Parent.Guides;
using VaccineApp.ViewModels.Parent.VaccineNotifications;
using VaccineApp.ViewModels.Supervisor;
using VaccineApp.ViewModels.Supervisor.Announcements;
using VaccineApp.ViewModels.Supervisor.Charts;
using VaccineApp.ViewModels.Supervisor.Periods;
using VaccineApp.ViewModels.Supervisor.Reports;
using VaccineApp.Views.Access.ForgotPassword;
using VaccineApp.Views.Access.SignIn;
using VaccineApp.Views.Admin.Home.Cluster;
using VaccineApp.Views.Admin.Home.Team;
using VaccineApp.Views.Admin.User;
using VaccineApp.Views.App;
using VaccineApp.Views.App.AboutUs;
using VaccineApp.Views.App.HelpSupport;
using VaccineApp.Views.App.Profile;
using VaccineApp.Views.App.Settings;
using VaccineApp.Views.Mobilizer.Announcements;
using VaccineApp.Views.Mobilizer.Home.Area.Clinic;
using VaccineApp.Views.Mobilizer.Home.Area.Doctor;
using VaccineApp.Views.Mobilizer.Home.Area.Influencer;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;
using VaccineApp.Views.Mobilizer.Home.Area.School;
using VaccineApp.Views.Mobilizer.Home.Family;
using VaccineApp.Views.Mobilizer.Home.Family.Child;
using VaccineApp.Views.Mobilizer.Home.Status;
using VaccineApp.Views.Mobilizer.Home.Status.AnonymousChild;
using VaccineApp.Views.Mobilizer.Home.Status.Vaccine;
using VaccineApp.Views.Parent;
using VaccineApp.Views.Parent.Guides;
using VaccineApp.Views.Parent.QR;
using VaccineApp.Views.Parent.VaccineNotifications;
using VaccineApp.Views.Supervisor;
using VaccineApp.Views.Supervisor.Announcements;
using VaccineApp.Views.Supervisor.Charts;
using VaccineApp.Views.Supervisor.Periods;
using VaccineApp.Views.Supervisor.Reports;

namespace VaccineApp.Factory;

public static class AddServiceExtensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddSingleton<ReportsGenerator>();

        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection Services)
    {
        Services.AddTransient<MasjeedListViewModel>();
        Services.AddTransient<SignInViewModel>();
        Services.AddTransient<FeedbackViewModel>();
        Services.AddTransient<AddChildViewModel>();
        Services.AddTransient<AddClusterViewModel>();
        Services.AddTransient<AddTeamViewModel>();
        Services.AddTransient<ClustersListViewModel>();
        Services.AddTransient<TeamsListViewModel>();
        Services.AddTransient<AddFamilyViewModel>();
        Services.AddTransient<FamiliesListViewModel>();
        Services.AddTransient<AddUserViewModel>();
        Services.AddTransient<ClinicsListViewModel>();
        Services.AddTransient<DoctorsListViewModel>();
        Services.AddTransient<InfluencersListViewModel>();
        Services.AddTransient<SchoolsListViewModel>();
        Services.AddTransient<AddClinicViewModel>();
        Services.AddTransient<AddDoctorViewModel>();
        Services.AddTransient<AddInfluencerViewModel>();
        Services.AddTransient<AddMasjeedViewModel>();
        Services.AddTransient<AddSchoolViewModel>();
        Services.AddTransient<ProfileViewModel>();
        Services.AddTransient<ParentFamilyViewModel>();
        Services.AddTransient<EditProfileViewModel>();
        Services.AddTransient<FamilyDetailsViewModel>();
        Services.AddTransient<UsersListViewModel>();
        Services.AddTransient<AboutUsViewModel>();
        Services.AddTransient<SettingsViewModel>();
        Services.AddTransient<StatsViewModel>();
        Services.AddTransient<AnnouncementsListViewModel>();
        Services.AddTransient<AddAnnouncementViewModel>();
        Services.AddTransient<AnnouncementListViewModel>();
        Services.AddTransient<PeriodsListViewModel>();
        Services.AddTransient<AddPeriodViewModel>();
        Services.AddTransient<AnonymousChildrenListViewModel>();
        Services.AddTransient<AddAnonymousChildViewModel>();
        Services.AddTransient<StatusViewModel>();
        Services.AddTransient<ChildDetailsViewModel>();
        Services.AddTransient<ReportsViewModel>();
        Services.AddTransient<AddVaccineViewModel>();
        Services.AddTransient<ChartsViewModel>();
        Services.AddTransient<ForgotPasswordViewModel>();
        Services.AddTransient<VaccinesTimeTableViewModel>();
        Services.AddTransient<VaccineGuidesViewModel>();
        Services.AddTransient<SchoolDetailsViewModel>();
        Services.AddTransient<ClinicDetailsViewModel>();
        Services.AddTransient<MasjeedDetailsViewModel>();
        Services.AddTransient<VaccineDetailsViewModel>();
        Services.AddTransient<ParentChildDetailsViewModel>();
        Services.AddTransient<VaccinesNotificationsViewModel>();

        return Services;
    }

    public static IServiceCollection AddPages(this IServiceCollection services)
    {
        // AccessShell Pages
        services.AddSingleton<ForgotPasswordPage>();
        services.AddSingleton<SignInPage>();

        // Admin Pages
        services.AddSingleton<AddClusterPage>();
        services.AddSingleton<ClustersListPage>();
        services.AddSingleton<AddTeamPage>();
        services.AddSingleton<TeamsListPage>();
        services.AddSingleton<AddUserPage>();
        services.AddSingleton<UsersListPage>();

        // App Pages
        services.AddSingleton<AboutUsPage>();
        services.AddSingleton<FeedbackPage>();
        services.AddSingleton<HelpSupportPage>();
        services.AddSingleton<EditProfilePage>();
        services.AddSingleton<ProfilePage>();
        services.AddSingleton<SettingsPage>();

        // Mobilizer Pages
        services.AddSingleton<AnnouncementListPage>();
        services.AddSingleton<AddClinicPage>();
        services.AddSingleton<ClinicDetailsPage>();
        services.AddSingleton<ClinicsListPage>();
        services.AddSingleton<AddDoctorPage>();
        services.AddSingleton<DoctorsListPage>();
        services.AddSingleton<AddInfluencerPage>();
        services.AddSingleton<InfluencersListPage>();
        services.AddSingleton<AddMasjeedPage>();
        services.AddSingleton<MasjeedsListPage>();
        services.AddSingleton<AddSchoolPage>();
        services.AddSingleton<SchoolsListPage>();
        services.AddSingleton<AddChildPage>();
        services.AddSingleton<AddFamilyPage>();
        services.AddSingleton<FamiliesListPage>();
        services.AddSingleton<FamilyDetailsPage>();
        services.AddSingleton<AddAnonymousChildPage>();
        services.AddSingleton<AnonymousChildrenListPage>();
        services.AddSingleton<AddVaccinePage>();
        services.AddSingleton<VaccineDetailsPage>();
        services.AddSingleton<ChildDetailsPage>();
        services.AddSingleton<StatusPage>();

        // Parent Pages
        services.AddSingleton<VaccineGuidesPage>();
        services.AddSingleton<VaccinesTimeTablePage>();
        services.AddSingleton<QRGeneratedImagePage>();
        services.AddSingleton<VaccineNotificationsPage>();
        services.AddSingleton<ParentChildDetailsPage>();
        services.AddSingleton<ParentFamilyPage>();

        // Supervisor Pages
        services.AddSingleton<AddAnnouncementPage>();
        services.AddSingleton<AnnouncementsListPage>();
        services.AddSingleton<ChartsPage>();
        services.AddSingleton<AddPeriodPage>();
        services.AddSingleton<PeriodsListPage>();
        services.AddSingleton<ReportsPage>();
        services.AddSingleton<StatsPage>();

        return services;
    }

    public static IServiceCollection AddDeviceSpecificFeatures(this IServiceCollection Services)
    {
        Services.AddSingleton<IToast, Toaster>();

        return Services;
    }
}