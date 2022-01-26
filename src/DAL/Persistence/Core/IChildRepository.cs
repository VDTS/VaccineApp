namespace DAL.Persistence.Core;
public interface IChildRepository <T> where T : class
{
    public Task<T> AddChild(T child, string familyId);
    public Task<IEnumerable<T>> GetChilds(string familyId);
}