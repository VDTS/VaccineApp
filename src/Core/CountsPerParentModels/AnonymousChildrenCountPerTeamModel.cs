namespace Core.CountsPerParentModels;

public class AnonymousChildrenCountPerTeamModel
{
    public string? TeamNo { get; set; }
    public IEnumerable<ChildrenGroupByType>? ChildrenByType { get; set; }
}

public class ChildrenGroupByType
{
    public string? ChildType { get; set; }
    public int Count { get; set; }
}
