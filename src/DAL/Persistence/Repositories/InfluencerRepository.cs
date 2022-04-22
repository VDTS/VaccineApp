using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class InfluencerRepository : IInfluencerRepository<InfluencerModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly DbNodePath _dbNodePath;

    public InfluencerRepository(IHttpClientFactory clientFactory, DbNodePath dbNodePath)
    {
        _clientFactory = clientFactory;
        _dbNodePath = dbNodePath;
    }

    public async Task<InfluencerModel> AddInfluencer(InfluencerModel influencer, string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(influencer);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(_dbNodePath.Influencer(teamId), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return influencer;
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

    public async Task<IEnumerable<InfluencerModel>> GetInfluencers(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, InfluencerModel>>(_dbNodePath.Influencer(teamId));

            return s != null ? s.Values.ToList() : Enumerable.Empty<InfluencerModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
