﻿using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class PeriodRepository : IPeriodRepository<PeriodModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly DbNodePath _dbNodePath;

    public PeriodRepository(IHttpClientFactory clientFactory, DbNodePath dbNodePath)
    {
        _clientFactory = clientFactory;
        _dbNodePath = dbNodePath;
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
            var activePeriod = await client.GetFromJsonAsync<Dictionary<string, PeriodModel>>($"{_dbNodePath.ActivePeriods()}");

            var s = await client.PostAsync(_dbNodePath.Periods(), byteContent);

            if (s.IsSuccessStatusCode)
            {
                if (activePeriod is not null)
                    await client.PutAsJsonAsync(_dbNodePath.PeriodActiveField(activePeriod.First().Key), false);

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
            var activePeriod = await client.GetFromJsonAsync<Dictionary<string, PeriodModel>>($"{_dbNodePath.ActivePeriods()}");

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
            var s = await client.GetFromJsonAsync<Dictionary<string, PeriodModel>>(_dbNodePath.Periods());

            return s != null ? s.Values.ToList() : Enumerable.Empty<PeriodModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
