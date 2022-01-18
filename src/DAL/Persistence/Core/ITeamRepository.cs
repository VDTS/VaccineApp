namespace DAL.Persistence.Core;
public interface ITeamRepository<T> where T : class
{
    public Task<T> AddTeam(T team);
    public Task<IEnumerable<T>> GetTeams();
}
