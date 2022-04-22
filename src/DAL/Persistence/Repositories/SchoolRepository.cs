﻿using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DAL.Persistence.Repositories;

public class SchoolRepository : ISchoolRepository<SchoolModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly DbNodePath _dbNodePath;

    public SchoolRepository(IHttpClientFactory clientFactory, DbNodePath dbNodePath)
    {
        _clientFactory = clientFactory;
        _dbNodePath = dbNodePath;
    }

    public async Task<SchoolModel> AddSchool(SchoolModel school, string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(school);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(_dbNodePath.School(teamId), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return school;
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

    public async Task<IEnumerable<SchoolModel>> GetSchools(string teamId)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var s = await client.GetFromJsonAsync<Dictionary<string, SchoolModel>>(_dbNodePath.School(teamId));

            return s != null ? s.Values.ToList() : Enumerable.Empty<SchoolModel>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
