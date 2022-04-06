using Core.Models;

namespace Core.HybridModels;

public class ChildrenCountPerVaccineStatusPerTeam : List<ChildrenCountPerVaccineStatus>
{
    public string TeamNo { get; private set; }
    public ChildrenCountPerVaccineStatusPerTeam(string teamNo, List<ChildrenCountPerVaccineStatus> childrenCountPerVaccineStatus) : base(childrenCountPerVaccineStatus)
    {
        TeamNo = teamNo;
    }
}

public class ChildrenCountPerVaccineStatus
{
    public string? VaccineStatus { get; set; }
    public int ChildrenCount { get; set; }
}

public class ChildrenWithVaccineStatus
{
    public string? VaccineStatus { get; set; }
    public ChildModel? Children { get; set; }
}
