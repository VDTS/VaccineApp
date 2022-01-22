﻿namespace DAL.Persistence.Core;

public interface IClinicRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetClinics(string teamId);
}
