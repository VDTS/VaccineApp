using Core.Models;
using DAL.Persistence.Core;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class DoctorRepository : IDoctorRepository<DoctorModel>
{
    private readonly IHttpClientFactory _clientFactory;
    public DoctorRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<DoctorModel>> GetDoctors(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, DoctorModel>>(DbNodePath.Doctor(teamId));

            return s.Values.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
