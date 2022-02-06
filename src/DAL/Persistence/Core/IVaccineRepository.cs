namespace DAL.Persistence.Core;

public interface IVaccineRepository<T> where T: class
{
    public Task<T> AddVaccine(T vaccine, string childId);
    public Task<IEnumerable<T>> GetVaccines(string childId);
}
