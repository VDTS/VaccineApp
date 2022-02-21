namespace DAL.Persistence;
public static class DbNodePath
{

    // This method return string starts with ? to add this parameter
    public static string ParamsForEnvironment()
    {
        if (DALConfigs.Env == "offline")
        {
            return "?ns=vaccineapp-2022";
        }
        else if (DALConfigs.Env == "online")
        {
            return "";
        }
        else
        {
            return "";
        }
    }

    // This method return string starts with & to add to other parameters
    public static string AddParamsForEnvironment()
    {
        if (DALConfigs.Env == "offline")
        {
            return "&ns=vaccineapp-2022";
        }
        else if (DALConfigs.Env == "online")
        {
            return "";
        }
        else
        {
            return "";
        }
    }
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

    public static string Masjeed(string teamId) => $"Masjeed/{teamId}.json{ParamsForEnvironment()}";
    public static string Child(string familyId) => $"Child/{familyId}.json{ParamsForEnvironment()}";
    public static string Cluster() => $"Cluster.json{ParamsForEnvironment()}";
    public static string Team(string Id)
    {
        return $"Team/{Id}.json{ParamsForEnvironment()}";
    }
    public static string Family(string teamId)
    {
        return $"Family/{teamId}.json{ParamsForEnvironment()}";
    }
    public static string Clinic(string teamId)
    {
        return $"Clinic/{teamId}.json{ParamsForEnvironment()}";
    }
    public static string Doctor(string teamId)
    {
        return $"Doctor/{teamId}.json{ParamsForEnvironment()}";
    }
    public static string Influencer(string teamId)
    {
        return $"Influencer/{teamId}.json{ParamsForEnvironment()}";
    }
    public static string School(string teamId)
    {
        return $"School/{teamId}.json{ParamsForEnvironment()}";
    }

    public static string Announcement() => $"Announcement.json{ParamsForEnvironment()}";
    public static string Periods() => $"Period.json{ParamsForEnvironment()}";
    public static string AnonymousChild(string teamId) => $"AnonymousChild{teamId}.json{ParamsForEnvironment()}";
    public static string Vaccine(string childId) => $"Vaccine/{childId}.json{ParamsForEnvironment()}";


    public static string ActivePeriods() => $"{Periods()}?orderBy=\"IsActive\"&equalTo=true{AddParamsForEnvironment()}";
    public static string PeriodActiveField(string periodFID) => $"Period/{periodFID}/IsActive.json{ParamsForEnvironment()}";

}
