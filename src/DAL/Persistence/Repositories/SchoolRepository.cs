using Core.Models;
using DAL.Persistence.Core;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class SchoolRepository : ISchoolRepository<SchoolModel>
{
    private readonly IHttpClientFactory _clientFactory;
    public SchoolRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<SchoolModel>> GetSchools(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, SchoolModel>>(DbNodePath.School(teamId));

            return s.Values.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
