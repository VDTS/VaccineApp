using Core.Models;
using DAL.Persistence.Core;
using DAL.Persistence.Repositories;
using DAL.Repositories;

namespace DAL.Persistence;

public class UnitOfWork
{
    private readonly IMasjeedRepository<MasjeedModel> _masjeedRepository;
    private readonly ChildRepository _childRepository;

    public UnitOfWork(MasjeedRepository masjeedRepository, ChildRepository childRepository)
    {
        _masjeedRepository = masjeedRepository;
        _childRepository = childRepository;
    }

    public async Task<IEnumerable<MasjeedModel>> GetMasjeeds()
    {
        return await _masjeedRepository.GetMasjeeds();
    }

    public async Task<ChildModel> AddChild(ChildModel child, string familyId)
    {
        return await _childRepository.AddChild(child, familyId);
    }

}