namespace DAL.Persistence.Core;

public interface IAnonymousChildRepository<T> where T : class
{
    public Task<T> AddAnonymousChild(T anonymousChild);
    public Task<IEnumerable<T>> GetAnonymousChildren();
}
