﻿using Core.Models;
using DAL.Persistence.Core;
using Newtonsoft.Json;

namespace DAL.Persistence.Repositories;
public class TeamRepository : ITeamRepository<TeamModel>
{
    private readonly IHttpClientFactory _clientFactory;

    public TeamRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<TeamModel> AddTeam(TeamModel team)
    {
        var client = _clientFactory.CreateClient("meta");

        try
        {
            var content = JsonConvert.SerializeObject(team);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var s = await client.PostAsync(DbNodePath.Team(), byteContent);

            if (s.IsSuccessStatusCode)
            {
                return team;
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