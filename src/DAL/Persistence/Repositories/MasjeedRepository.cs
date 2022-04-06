using DAL.Persistence.Core;
using System.Net.Http.Json;
using Core.Models;
using DAL.Persistence;
using Newtonsoft.Json;

namespace DAL.Repositories;

public class MasjeedRepository : IMasjeedRepository<MasjeedModel>
{
    private readonly IHttpClientFactory _clientFactory;
    public MasjeedRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<MasjeedModel> AddMasjeed(MasjeedModel masjeed, string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(masjeed);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(DbNodePath.Masjeed(teamId), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return masjeed;
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

    public async Task<IEnumerable<MasjeedModel>> GetMasjeeds(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, MasjeedModel>>(DbNodePath.Masjeed(teamId));

            return s != null ? s.Values.ToList() : Enumerable.Empty<MasjeedModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}