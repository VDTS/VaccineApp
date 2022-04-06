using Core.Models;

namespace Core.HybridModels;

public class ChildWithNextVaccineModel
{
    public ChildModel? Child { get; set; }
    public string? NextVaccine { get; set; }
}
