namespace DAL.Persistence;
public static class DbNodePath
{
    private static string TeamId = Preferences.Get("TeamId", "AnonymousTeam");
    private static string ClusterId = Preferences.Get("ClusterId", "AnonymousCluster");
    // VaccineApp NoSQL Schema

    // Cluster
    //      ClusterIds

    // ClusterIds
    //      TeamIds

    // TeamId
    //      Masjeeds
    //      Family
    //              FamilyIds

    // FamilyId
    //      ChildIds

    // ChildId
    //      VaccineIds

    public static string Masjeed() => $"Masjeed/{TeamId}.json";
    public static string Child(string FamilyId) => $"Child/{FamilyId}.json";
    public static string Cluster() => $"Cluster.json";
    public static string Team(string Id = null)
    {
        if (string.IsNullOrEmpty(Id))
        {
            return $"Team/{ClusterId}.json";
        }
        return $"Team/{Id}.json";
    }
    public static string Family(string teamId)
    {
        if (string.IsNullOrEmpty(teamId))
        {
            return $"Family/{TeamId}.json";
        }
        else
        {
            return $"Family/{teamId}.json";
        }
    }
}
