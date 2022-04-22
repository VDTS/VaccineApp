using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;
public class FamilyRepository : IFamilyRepository<FamilyModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly DbNodePath _dbNodePath;

    public FamilyRepository(IHttpClientFactory clientFactory, DbNodePath dbNodePath)
    {
        _clientFactory = clientFactory;
        _dbNodePath = dbNodePath;
    }
    public async Task<FamilyModel> AddFamily(FamilyModel family, string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(family);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(_dbNodePath.Family(teamId), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return family;
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

    public async Task<IEnumerable<FamilyModel>> GetFamilies(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, FamilyModel>>(_dbNodePath.Family(teamId));

            return s != null ? s.Values.ToList() : Enumerable.Empty<FamilyModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<FamilyModel> GetParentFamily(string teamId, string familyId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, FamilyModel>>(_dbNodePath.Family(teamId));

            return s is not null ? s.Values.First(x => x.Id.ToString() == familyId) : throw new Exception("No family attached to this accound");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
