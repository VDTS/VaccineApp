using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class ClinicRepository : IClinicRepository<ClinicModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly DbNodePath _dbNodePath;

    public ClinicRepository(IHttpClientFactory clientFactory, DbNodePath dbNodePath)
    {
        _clientFactory = clientFactory;
        _dbNodePath = dbNodePath;
    }

    public async Task<ClinicModel> AddClinic(ClinicModel clinic, string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(clinic);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(_dbNodePath.Clinic(teamId), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return clinic;
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

    public async Task<IEnumerable<ClinicModel>> GetClinics(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, ClinicModel>>(_dbNodePath.Clinic(teamId));

            return s != null ? s.Values.ToList() : Enumerable.Empty<ClinicModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
