using DAL.Persistence;
using DAL.Persistence.Repositories;
using DAL.Repositories;

namespace DAL.Factory;

public static class DbClientConfigurationExtension
{
    public static IServiceCollection AddDAL(
        this IServiceCollection service,
        Action<DALSettings> Configure)
    {
        var appSettings = new DALSettings();

        Configure(appSettings);

        DALConfigs.FirebaseApiKey = appSettings.FirebaseApiKey;
        DALConfigs.FirebaseBaseAddress = appSettings.FirebaseBaseAddress;

        service.AddSingleton<UnitOfWork>();
        service.AddSingleton<MasjeedRepository>();
        service.AddSingleton<ChildRepository>();
        service.AddSingleton<ClusterRepository>();
        service.AddSingleton<TeamRepository>();
        service.AddSingleton<FamilyRepository>();
        service.AddSingleton<ClinicRepository>();
        service.AddSingleton<DoctorRepository>();
        service.AddSingleton<InfluencerRepository>();
        service.AddSingleton<SchoolRepository>();
        service.AddSingleton<AnnouncementRepository>();
        service.AddSingleton<PeriodRepository>();
        service.AddSingleton<AnonymousChildRepository>();

        service.AddHttpClient();
        service.AddHttpClient("meta", c =>
        {
            c.BaseAddress = new Uri(DALConfigs.FirebaseBaseAddress);
        });
        return service;
    }
}