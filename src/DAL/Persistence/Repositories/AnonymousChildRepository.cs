using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class AnonymousChildRepository : IAnonymousChildRepository<AnonymousChildModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly DbNodePath _dbNodePath;

    public AnonymousChildRepository(IHttpClientFactory clientFactory, DbNodePath dbNodePath)
    {
        _clientFactory = clientFactory;
        _dbNodePath = dbNodePath;
    }

    public async Task<AnonymousChildModel> AddAnonymousChild(AnonymousChildModel anonymousChild, string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(anonymousChild);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(_dbNodePath.AnonymousChild(teamId), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return anonymousChild;
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

    public async Task<IEnumerable<AnonymousChildModel>> GetAnonymousChildren(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, AnonymousChildModel>>(_dbNodePath.AnonymousChild(teamId));

            return s != null ? s.Values.ToList() : Enumerable.Empty<AnonymousChildModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
