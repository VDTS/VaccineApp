namespace DAL.Persistence.Core;
public interface IClusterRepository<T> where T : class
{
    public Task<T> AddCluster(T cluster);
    public Task<IEnumerable<T>> GetClusters();
}
