namespace DAL.Persistence.Core;

public interface ISchoolRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetSchools(string teamId);
    public Task<T> AddSchool(T school, string teamId);
}