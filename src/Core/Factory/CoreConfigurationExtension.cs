using Core.Features;
using Core.Models;

namespace Core.Factory;
public static class CoreConfigurationExtension
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection service)
    {
        //Add Services
        service.AddAutoMapper(typeof(IAssemblyApi));

        // Device Features
        service.AddSingleton<IToast, Toaster>();

        // Models
        service.AddTransient<ClusterModel>();
        service.AddTransient<TeamModel>();
        service.AddTransient<FamilyModel>();
        service.AddTransient<ChildModel>();
        service.AddTransient<ClinicModel>();
        service.AddTransient<DoctorModel>();
        service.AddTransient<InfluencerModel>();
        service.AddTransient<SchoolModel>();
        service.AddTransient<MasjeedModel>();
        service.AddTransient<ProfileModel>();
        service.AddTransient<EditProfileModel>();

        return service;
    }
}