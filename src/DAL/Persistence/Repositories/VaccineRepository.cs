using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;

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

            if (s.IsSuccessStatusCode)
            {
                return vaccine;
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
}
