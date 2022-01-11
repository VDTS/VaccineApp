using DAL.Persistence.Core;
using System.Net.Http.Json;
using Core.Models;

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
            var s = await client.GetFromJsonAsync<Dictionary<string, MasjeedModel>>("Kandahar-Area/Masjeed/35d41d49-eeb8-40a1-a512-13499fcbd7fb.json");

            return s.Values.ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}