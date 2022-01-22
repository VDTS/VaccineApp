using Core.Models;
using DAL.Persistence.Core;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class InfluencerRepository : IInfluencerRepository<InfluencerModel>
{
    private readonly IHttpClientFactory _clientFactory;
    public InfluencerRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<InfluencerModel>> GetInfluencers(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, InfluencerModel>>(DbNodePath.Influencer(teamId));

            return s.Values.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
