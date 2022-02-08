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
using VaccineApp.ViewModels.Supervisor;
using VaccineApp.ViewModels.Supervisor.Announcements;
using VaccineApp.ViewModels.Supervisor.Charts;
using VaccineApp.ViewModels.Supervisor.Periods;
using VaccineApp.ViewModels.Supervisor.Reports;

namespace VaccineApp.Factory;

public static class AddServiceExtensions
{
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

        return Services;
    }
}