using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class VaccineRepository : IVaccineRepository<VaccineModel>
{
    private readonly IHttpClientFactory _clientFactory;

    public VaccineRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    public async Task<VaccineModel> AddVaccine(VaccineModel vaccine, string childId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(vaccine);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(DbNodePath.Vaccine(childId), byteContent);

            return s.IsSuccessStatusCode ? vaccine : throw new Exception(s.ReasonPhrase);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<VaccineModel>> GetVaccines(string childId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, VaccineModel>>(DbNodePath.Vaccine(childId));

            return s != null ? s.Values.ToList() : Enumerable.Empty<VaccineModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
