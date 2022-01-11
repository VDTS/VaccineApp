using Core.Models;
using DAL.Persistence.Core;
using DAL.Repositories;

namespace DAL.Persistence;

public class UnitOfWork
{
    private readonly IMasjeedRepository<MasjeedModel> masjeedRepository;

    public UnitOfWork(MasjeedRepository masjeedRepository)
    {
        this.masjeedRepository = masjeedRepository;
    }

    public async Task<IEnumerable<MasjeedModel>> GetMasjeeds()
    {
        return await masjeedRepository.GetMasjeeds();
    }

}