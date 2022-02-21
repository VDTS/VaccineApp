namespace Core.StaticData;

public class VaccineStatus
{
    public static List<string> ListStatuses()
    {
        return new List<string>
        {
            "+", "(+)", "V", "(V)", "O", "Blank", "NSS", "NSS+", "NSSV", "FM+", "FMV", "RRC+", "RRCV", "MRC+", "MRCV", "-->"
        };
    }
}
