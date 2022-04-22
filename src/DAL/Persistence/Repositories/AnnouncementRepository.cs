using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class AnnouncementRepository : IAnnouncementRepository<AnnouncementModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly DbNodePath _dbNodePath;

    public AnnouncementRepository(IHttpClientFactory clientFactory, DbNodePath dbNodePath)
    {
        _clientFactory = clientFactory;
        _dbNodePath = dbNodePath;
    }

    public async Task<AnnouncementModel> AddAnnouncement(AnnouncementModel announcement)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(announcement);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(_dbNodePath.Announcement(), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return announcement;
            }
            else
            {
                throw new Exception(s.ReasonPhrase);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<AnnouncementModel>> GetAnnouncements()
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, AnnouncementModel>>(_dbNodePath.Announcement());

            return s != null ? s.Values.ToList() : Enumerable.Empty<AnnouncementModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
