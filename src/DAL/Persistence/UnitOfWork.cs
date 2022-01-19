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

    public UnitOfWork(
        MasjeedRepository masjeedRepository,
        ChildRepository childRepository,
        ClusterRepository clusterRepository,
        TeamRepository teamRepository,
        FamilyRepository familyRepository)
    {
        _masjeedRepository = masjeedRepository;
        _childRepository = childRepository;
        _clusterRepository = clusterRepository;
        _teamRepository = teamRepository;
        _familyRepository = familyRepository;
    }

    public async Task<IEnumerable<MasjeedModel>> GetMasjeeds()
    {
        return await _masjeedRepository.GetMasjeeds();
    }

    public async Task<ChildModel> AddChild(ChildModel child, string familyId)
    {
        return await _childRepository.AddChild(child, familyId);
    }

    public async Task<ClusterModel> AddCluster(ClusterModel cluster)
    {
        return await _clusterRepository.AddCluster(cluster);
    }

    public async Task<TeamModel> AddTeam(TeamModel teamModel, string Id)
    {
        return await _teamRepository.AddTeam(teamModel, Id);
    }

    public async Task<IEnumerable<ClusterModel>> GetClusters()
    {
        return await _clusterRepository.GetClusters();
    }

    public async Task<IEnumerable<TeamModel>> GetTeams(string clusterId = null)
    {
        return await _teamRepository.GetTeams(clusterId);
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
        return await _familyRepository.AddFamily(family, teamId);
    }
}