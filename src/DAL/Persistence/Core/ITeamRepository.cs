namespace DAL.Persistence.Core;
public interface ITeamRepository<T> where T : class
{
    public Task<T> AddTeam(T team, string Id);
    public Task<IEnumerable<T>> GetTeams(string clusterId);
}
