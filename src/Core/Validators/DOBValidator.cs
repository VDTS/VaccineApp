namespace Core.Validators;

public class DOBValidator
{
    public static bool IsChildEligibleForVaccine(DateTime DOB)
    {
        var ageInMonths = 12 * (DateTime.UtcNow.Year - DOB.Year) + DOB.Month;
        if (ageInMonths <= 60)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
