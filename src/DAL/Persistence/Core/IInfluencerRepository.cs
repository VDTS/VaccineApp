﻿namespace DAL.Persistence.Core;

public interface IInfluencerRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetInfluencers(string teamId);
}
