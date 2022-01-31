using Core.Models;
using DAL.Persistence.Core;
using DAL.Persistence.Repositories;
using DAL.Repositories;

namespace DAL.Persistence;

public class UnitOfWork
{
    private readonly IMasjeedRepository<MasjeedModel> _masjeedRepository;
    private readonly ChildRepository _childRepository;
    private readonly ClusterRepository _clusterRepository;
    private readonly TeamRepository _teamRepository;
    private readonly FamilyRepository _familyRepository;
    private readonly ClinicRepository _clinicRepository;
    private readonly DoctorRepository _doctorRepository;
    private readonly InfluencerRepository _influencerRepository;
    private readonly SchoolRepository _schoolRepository;
    private readonly AnnouncementRepository _announcementRepository;
    private string _clusterId;
    private string _teamId;
    private string _familyId;

    public UnitOfWork(
        MasjeedRepository masjeedRepository,
        ChildRepository childRepository,
        ClusterRepository clusterRepository,
        TeamRepository teamRepository,
        FamilyRepository familyRepository,
        ClinicRepository clinicRepository,
        DoctorRepository doctorRepository,
        InfluencerRepository influencerRepository,
        SchoolRepository schoolRepository,
        AnnouncementRepository announcementRepository)
    {
        _masjeedRepository = masjeedRepository;
        _childRepository = childRepository;
        _clusterRepository = clusterRepository;
        _teamRepository = teamRepository;
        _familyRepository = familyRepository;
        _clinicRepository = clinicRepository;
        _doctorRepository = doctorRepository;
        _influencerRepository = influencerRepository;
        _schoolRepository = schoolRepository;
        _announcementRepository = announcementRepository;
        GetClaims();
    }

    private void GetClaims()
    {
        _clusterId = Preferences.Get("ClusterId", "AnonymousCluster");
        _teamId = Preferences.Get("TeamId", "AnonymousTeam");
        _familyId = Preferences.Get("FamilyId", "AnonymousFamily");
    }
    
    public async Task<IEnumerable<MasjeedModel>> GetMasjeeds()
    {
        return await _masjeedRepository.GetMasjeeds(_teamId);
    }

    public async Task<ChildModel> AddChild(ChildModel child, string familyId)
    {
        if (familyId == null)
        {
            return await _childRepository.AddChild(child, _familyId);
        }
        else
        {
            return await _childRepository.AddChild(child, familyId);
        }
    }

    public async Task<ClusterModel> AddCluster(ClusterModel cluster)
    {
        return await _clusterRepository.AddCluster(cluster);
    }

    public async Task<TeamModel> AddTeam(TeamModel teamModel, string Id)
    {
        if (Id == null)
        {
            return await _teamRepository.AddTeam(teamModel, _clusterId);
        }
        else
        {
            return await _teamRepository.AddTeam(teamModel, Id);
        }
    }

    public async Task<IEnumerable<ClusterModel>> GetClusters()
    {
        return await _clusterRepository.GetClusters();
    }

    public async Task<IEnumerable<TeamModel>> GetTeams(string clusterId = null)
    {
        if (clusterId == null)
        {
            return await _teamRepository.GetTeams(_clusterId);
        }
        else
        {
            return await _teamRepository.GetTeams(clusterId);
        }
    }

    public async Task<IEnumerable<TeamModel>> GetAllTeams()
    {
        var allTeams = new List<TeamModel>();
        var clusterId = await _clusterRepository.GetClusters();

        foreach (var item in clusterId)
        {
            var teams = await GetTeams(item.Id.ToString());
            allTeams.AddRange(teams);
        }

        return allTeams;
    }
    public async Task<FamilyModel> AddFamily(FamilyModel family, string teamId = null)
    {
        if (teamId == null)
        {
            return await _familyRepository.AddFamily(family, _teamId);
        }
        else
        {
            return await _familyRepository.AddFamily(family, teamId);
        }
    }

    public async Task<IEnumerable<FamilyModel>> GetFamilies(string teamId = null)
    {
        if (teamId == null)
        {
            return await _familyRepository.GetFamilies(_teamId);
        }
        else
        {
            return await _familyRepository.GetFamilies(teamId);
        }
    }

    public async Task<IEnumerable<ClinicModel>> GetClinics()
    {
        return await _clinicRepository.GetClinics(_teamId);
    }

    public async Task<IEnumerable<DoctorModel>> GetDoctors()
    {
        return await _doctorRepository.GetDoctors(_teamId);
    }

    public async Task<IEnumerable<InfluencerModel>> GetInfluencers()
    {
        return await _influencerRepository.GetInfluencers(_teamId);
    }

    public async Task<IEnumerable<SchoolModel>> GetSchools()
    {
        return await _schoolRepository.GetSchools(_teamId);
    }

    public async Task<ClinicModel> AddClinic(ClinicModel clinic)
    {
        return await _clinicRepository.AddClinic(clinic, _teamId);
    }

    public async Task<DoctorModel> AddDoctor(DoctorModel doctor)
    {
        return await _doctorRepository.AddDoctor(doctor, _teamId);
    }

    public async Task<InfluencerModel> AddInfluencer(InfluencerModel influencer)
    {
        return await _influencerRepository.AddInfluencer(influencer, _teamId);
    }

    public async Task<MasjeedModel> AddMasjeed(MasjeedModel masjeed)
    {
        return await _masjeedRepository.AddMasjeed(masjeed, _teamId);
    }

    public async Task<SchoolModel> AddSchool(SchoolModel school)
    {
        return await _schoolRepository.AddSchool(school, _teamId);
    }

    public async Task<IEnumerable<ChildModel>> GetChilds(string familyId)
    {
        return await _childRepository.GetChilds(familyId);
    }

    public async Task<AnnouncementModel> AddAnnouncement(AnnouncementModel announcement)
    {
        return await _announcementRepository.AddAnnouncement(announcement);
    }

    public async Task<IEnumerable<AnnouncementModel>> GetAnnouncements()
    {
        return await _announcementRepository.GetAnnouncements();
    }
}