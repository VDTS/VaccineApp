namespace DAL.Persistence.Core;

public interface IAnonymousChildRepository<T> where T : class
{
    public Task<T> AddAnonymousChild(T anonymousChild, string teamId);
    public Task<IEnumerable<T>> GetAnonymousChildren(string teamId);
}
