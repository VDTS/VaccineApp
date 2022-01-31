namespace DAL.Persistence.Core;

public interface IAnnouncementRepository<T> where T : class
{
    public Task<T> AddAnnouncement(T announcement);
    public Task<IEnumerable<T>> GetAnnouncements();
}
