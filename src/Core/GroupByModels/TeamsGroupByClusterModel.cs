using Core.Models;

namespace Core.GroupByModels;

public class TeamsGroupByClusterModel : List<TeamModel>
{
    public string ClusterName { get; private set; }
    public TeamsGroupByClusterModel(string clusterName, List<TeamModel> teams) : base(teams)
    {
        ClusterName = clusterName;
    }
}