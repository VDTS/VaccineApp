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
    public static string Clinic(string teamId)
    {
        return $"Clinic/{teamId}.json";
    }
    public static string Doctor(string teamId)
    {
        return $"Doctor/{teamId}.json";
    }
    public static string Influencer(string teamId)
    {
        return $"Influencer/{teamId}.json";
    }
    public static string School(string teamId)
    {
        return $"School/{teamId}.json";
    }

    public static string Announcement() => $"Announcement.json";
}
