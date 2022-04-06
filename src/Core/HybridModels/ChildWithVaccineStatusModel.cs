using Core.Models;

namespace Core.HybridModels;

public class ChildWithVaccineStatusModel
{
    public ChildModel? Child { get; set; }
    public string? VaccineStatus { get; set; }
}
