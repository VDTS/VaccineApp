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
            var contentId = JsonConvert.SerializeObject(period.Id);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);


            var bufferId = System.Text.Encoding.UTF8.GetBytes(contentId);
            var byteContentId = new ByteArrayContent(bufferId);

            var s = await client.PostAsync(DbNodePath.Periods(), byteContent);


            if (s.IsSuccessStatusCode)
            {
                var p = await client.PutAsync(DbNodePath.CurrentConfig(), byteContentId);
                if (p.IsSuccessStatusCode)
                {
                    return period;
                }
                else
                {
                    throw new Exception(s.ReasonPhrase);
                }
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

    public async Task<IEnumerable<PeriodModel>> GetPeriods()
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, PeriodModel>>(DbNodePath.Periods());

            return s.Values.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
