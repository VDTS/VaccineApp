using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;
public class ClusterRepository : IClusterRepository<ClusterModel>
{
    private readonly IHttpClientFactory _clientFactory;

    public ClusterRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<ClusterModel> AddCluster(ClusterModel cluster)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(cluster);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(DbNodePath.Cluster(), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return cluster;
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

    public async Task<IEnumerable<ClusterModel>> GetClusters()
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, ClusterModel>>(DbNodePath.Cluster());

            return s != null ? s.Values.ToList() : Enumerable.Empty<ClusterModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
