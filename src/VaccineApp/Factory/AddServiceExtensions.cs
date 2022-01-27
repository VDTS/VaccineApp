using VaccineApp.ViewModels.Access.SignIn;
using VaccineApp.ViewModels.Admin.Home.Cluster;
using VaccineApp.ViewModels.Admin.Home.Team;
using VaccineApp.ViewModels.Admin.Home.User;
using VaccineApp.ViewModels.App.AboutUs;
using VaccineApp.ViewModels.App.Feedback;
using VaccineApp.ViewModels.App.Profile;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Clinic;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Doctor;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Influencer;
using VaccineApp.ViewModels.Mobilizer.Home.Area.Masjeed;
using VaccineApp.ViewModels.Mobilizer.Home.Area.School;
using VaccineApp.ViewModels.Mobilizer.Home.Family;
using VaccineApp.ViewModels.Mobilizer.Home.Family.Child;
using VaccineApp.ViewModels.Parent;

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

        return Services;
    }
}