namespace DAL.Persistence.Core;

public interface IPeriodRepository<T> where T : class
{
    public Task<T> AddPeriod(T period);
    public Task<IEnumerable<T>> GetPeriods();
}
