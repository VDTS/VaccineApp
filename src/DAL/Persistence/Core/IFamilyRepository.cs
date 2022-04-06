namespace DAL.Persistence.Core;
public interface IFamilyRepository<T> where T : class
{
    public Task<T> AddFamily(T family, string teamId);
    public Task<IEnumerable<T>> GetFamilies(string teamId);
    public Task<T> GetParentFamily(string teamId, string familyId);
}
