namespace DAL.Persistence;
public static class DbNodePath
{
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

    public static string Masjeed(string teamId) => $"Masjeed/{teamId}.json";
    public static string Child(string familyId) => $"Child/{familyId}.json";
    public static string Cluster() => $"Cluster.json";
    public static string Team(string Id)
    {
        return $"Team/{Id}.json";
    }
    public static string Family(string teamId)
    {
        return $"Family/{teamId}.json";
    }
}
