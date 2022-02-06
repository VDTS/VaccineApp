namespace DAL.Persistence.Core;

public interface IVaccineRepository<T> where T: class
{
    public Task<T> AddVaccine(T vaccine, string childId);
}
