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

    public UnitOfWork(
        MasjeedRepository masjeedRepository,
        ChildRepository childRepository,
        ClusterRepository clusterRepository,
        TeamRepository teamRepository)
    {
        _masjeedRepository = masjeedRepository;
        _childRepository = childRepository;
        _clusterRepository = clusterRepository;
        _teamRepository = teamRepository;
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

    public async Task<TeamModel> AddTeam(TeamModel teamModel)
    {
        return await _teamRepository.AddTeam(teamModel);
    }

    public async Task<IEnumerable<ClusterModel>> GetClusters()
    {
        return await _clusterRepository.GetClusters();
    }
}