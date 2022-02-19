using Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Factory;
public static class CoreConfigurationExtension
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection service)
    {
        //Add Services
        service.AddAutoMapper(typeof(IAssemblyApi));

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