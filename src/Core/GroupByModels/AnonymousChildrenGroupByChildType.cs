using Core.Models;

namespace Core.GroupByModels;

public class AnonymousChildrenGroupByChildType : List<AnonymousChildModel>
{
    public string ChildType { get; private set; }
    public AnonymousChildrenGroupByChildType(string childType, List<AnonymousChildModel> AnonymousChild) : base(AnonymousChild)
    {
        ChildType = childType;
    }
}
