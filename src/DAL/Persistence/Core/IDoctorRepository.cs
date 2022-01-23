namespace DAL.Persistence.Core;

public interface IDoctorRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetDoctors(string teamId);
    public Task<T> AddDoctor(T doctor, string teamId);
}