namespace Core.Validators;

public class CommonPropertiesValidator
{
    public static bool ValidFullName(string name)
    {
        name = name.Replace(" ", "");
        return name.All(Char.IsLetter);
    }
}
