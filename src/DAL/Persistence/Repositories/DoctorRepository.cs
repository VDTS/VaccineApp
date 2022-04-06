using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class DoctorRepository : IDoctorRepository<DoctorModel>
{
    private readonly IHttpClientFactory _clientFactory;
    public DoctorRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<DoctorModel> AddDoctor(DoctorModel doctor, string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(doctor);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(DbNodePath.Doctor(teamId), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return doctor;
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

    public async Task<IEnumerable<DoctorModel>> GetDoctors(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, DoctorModel>>(DbNodePath.Doctor(teamId));

            return s != null ? s.Values.ToList() : Enumerable.Empty<DoctorModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
