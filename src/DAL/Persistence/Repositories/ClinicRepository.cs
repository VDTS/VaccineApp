using Core.Models;
using DAL.Persistence.Core;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class ClinicRepository : IClinicRepository<ClinicModel>
{
    private readonly IHttpClientFactory _clientFactory;
    public ClinicRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<ClinicModel>> GetClinics(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, ClinicModel>>(DbNodePath.Clinic(teamId));

            return s.Values.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
