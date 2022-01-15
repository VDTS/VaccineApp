using DAL.Persistence.Core;
using System.Net.Http.Json;
using Core.Models;
using DAL.Persistence;

namespace DAL.Repositories;

public class MasjeedRepository : IMasjeedRepository<MasjeedModel>
{
    private readonly IHttpClientFactory _clientFactory;
    public MasjeedRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<MasjeedModel>> GetMasjeeds()
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, MasjeedModel>>(DbNodePath.Masjeed());

            return s.Values.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}