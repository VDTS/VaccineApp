using Core.HybridModels;

namespace Core.GroupByModels;

public class ChildrenGroupByHouseNoModel : List<ChildWithVaccineStatusModel>
{
    public int HouseNo { get; private set; }
    public ChildrenGroupByHouseNoModel(int houseNo, List<ChildWithVaccineStatusModel> childs) : base(childs)
    {
        this.HouseNo = houseNo;
    }
}
