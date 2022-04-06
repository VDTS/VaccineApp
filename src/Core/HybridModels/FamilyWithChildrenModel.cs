using Core.Models;

namespace Core.HybridModels;

public class FamilyWithChildrenModel
{
    public FamilyModel? Family { get; set; }
    public IEnumerable<ChildModel>? Children { get; set; }
}
