namespace Core.Validators;

public class CommonPropertiesValidator
{
    public static bool ValidFullName(string? name)
    {
        if (name is not null)
        {
            name = name.Replace(" ", "");
            return name.All(Char.IsLetter);
        }
        return false;
    }
}
