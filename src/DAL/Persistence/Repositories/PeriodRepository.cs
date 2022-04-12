using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class PeriodRepository : IPeriodRepository<PeriodModel>
{
    private readonly IHttpClientFactory _clientFactory;
    public PeriodRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<PeriodModel> AddPeriod(PeriodModel period)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(period);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            // Fetch current active Period
            var activePeriod = await client.GetFromJsonAsync<Dictionary<string, PeriodModel>>($"{DbNodePath.ActivePeriods()}");

            var s = await client.PostAsync(DbNodePath.Periods(), byteContent);

            if (s.IsSuccessStatusCode)
            {
                if (activePeriod is not null)
                    await client.PutAsJsonAsync(DbNodePath.PeriodActiveField(activePeriod.First().Key), false);

                return period;
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

    public async Task<PeriodModel> GetActivePeriod()
    {
        var client = _clientFactory.CreateClient("meta");
        try
        {
            var activePeriod = await client.GetFromJsonAsync<Dictionary<string, PeriodModel>>($"{DbNodePath.ActivePeriods()}");

            return activePeriod is not null ? activePeriod.FirstOrDefault().Value : throw new Exception("No active period");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<PeriodModel>> GetPeriods()
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, PeriodModel>>(DbNodePath.Periods());

            return s != null ? s.Values.ToList() : Enumerable.Empty<PeriodModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
