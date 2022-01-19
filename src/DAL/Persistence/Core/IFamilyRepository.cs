namespace DAL.Persistence.Core;
public interface IFamilyRepository<T> where T : class
{
    public Task<T> AddFamily(T family, string teamId = null);
}
