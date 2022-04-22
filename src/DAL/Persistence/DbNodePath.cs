namespace DAL.Persistence;
public class DbNodePath
{

    // This method return string starts with ? to add this parameter
    public string ParamsForEnvironment()
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
    public string AddParamsForEnvironment()
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

    public string Masjeed(string teamId) => $"Masjeed/{teamId}.json{ParamsForEnvironment()}";
    public string Child(string familyId) => $"Child/{familyId}.json{ParamsForEnvironment()}";
    public string Cluster() => $"Cluster.json{ParamsForEnvironment()}";
    public string Team(string Id)
    {
        return $"Team/{Id}.json{ParamsForEnvironment()}";
    }
    public string Family(string teamId)
    {
        return $"Family/{teamId}.json{ParamsForEnvironment()}";
    }
    public string Clinic(string teamId)
    {
        return $"Clinic/{teamId}.json{ParamsForEnvironment()}";
    }
    public string Doctor(string teamId)
    {
        return $"Doctor/{teamId}.json{ParamsForEnvironment()}";
    }
    public string Influencer(string teamId)
    {
        return $"Influencer/{teamId}.json{ParamsForEnvironment()}";
    }
    public string School(string teamId)
    {
        return $"School/{teamId}.json{ParamsForEnvironment()}";
    }

    public string Announcement() => $"Announcement.json{ParamsForEnvironment()}";
    public string Periods() => $"Period.json{ParamsForEnvironment()}";
    public string AnonymousChild(string teamId) => $"AnonymousChild{teamId}.json{ParamsForEnvironment()}";
    public string Vaccine(string childId) => $"Vaccine/{childId}.json{ParamsForEnvironment()}";


    public string ActivePeriods() => $"{Periods()}?orderBy=\"IsActive\"&equalTo=true{AddParamsForEnvironment()}";
    public string PeriodActiveField(string periodFID) => $"Period/{periodFID}/IsActive.json{ParamsForEnvironment()}";

}
