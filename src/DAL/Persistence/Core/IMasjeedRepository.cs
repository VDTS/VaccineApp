namespace DAL.Persistence.Core;

public interface IMasjeedRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetMasjeeds();
}