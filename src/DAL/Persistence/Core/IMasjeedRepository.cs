namespace DAL.Persistence.Core;

public interface IMasjeedRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetMasjeeds(string teamId);
    public Task<T> AddMasjeed(T masjeed, string teamId);
}