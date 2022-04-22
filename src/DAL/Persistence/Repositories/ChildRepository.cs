using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;
public class ChildRepository : IChildRepository<ChildModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly DbNodePath _dbNodePath;

    public ChildRepository(IHttpClientFactory clientFactory, DbNodePath dbNodePath)
    {
        _clientFactory = clientFactory;
        _dbNodePath = dbNodePath;
    }
    public async Task<ChildModel> AddChild(ChildModel child, string familyId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(child);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(_dbNodePath.Child(familyId), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return child;
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

    public async Task<IEnumerable<ChildModel>> GetChilds(string familyId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, ChildModel>>(_dbNodePath.Child(familyId));

            return s != null ? s.Values.ToList() : Enumerable.Empty<ChildModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}