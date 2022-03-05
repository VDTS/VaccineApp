namespace Core.Utility;

public class NextVaccineFinder
{
    public static string FindNextVaccine(DateTime DOB)
    {
        var s = AgeDiff(DOB);

        if (s >= 1 && s <= 10)
        {
            return "BCG, OPV0, Hep B";
        }
        else if (s >= 41 && s <= 50)
        {
            return "Penta1, OPV1, PCV1, Rota 1";
        }
        else if (s >= 70 && s <= 72)
        {
            return "Penta2, OPV2, PCV2, Rota 2";
        }
        else if (s >= 98 && s <= 100)
        {
            return "Penta3, OPV3, PCV3, IPV";
        }
        else if (s >= 270 && s <= 272)
        {
            return "Measles 1, OPV 4";
        }
        else if (s >= 540 && s <= 531)
        {
            return "Measles 2";
        }

        return "No Vaccine";
    }

    public static double AgeDiff(DateTime DOB)
    {
        var now = DateTime.UtcNow;
        var s = now - DOB;

        return s.Days;
    }
}
