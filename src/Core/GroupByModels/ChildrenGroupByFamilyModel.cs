using Core.Models;

namespace Core.GroupByModels;

public class ChildrenGroupByHouseNoModel : List<ChildModel>
{
    public int HouseNo { get; private set; }
    public ChildrenGroupByHouseNoModel(int houseNo, List<ChildModel> childs) : base(childs)
    {
        this.HouseNo = houseNo;
    }
}
