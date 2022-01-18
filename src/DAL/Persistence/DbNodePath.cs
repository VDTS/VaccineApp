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

    public static string Masjeed() => $"{TeamId}/Masjeed.json";
    public static string Child(string FamilyId) => $"{FamilyId}/Child.json";
    public static string Cluster() => $"Cluster.json";
    public static string Team() => $"{ClusterId}/Team.json";
}
