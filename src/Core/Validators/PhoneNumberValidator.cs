using System.Text.RegularExpressions;

namespace Core.Validators;
public class PhoneNumberValidator
{
    public static bool IsPhoneNumberValid(string? phoneNumber)
    {
        if (!string.IsNullOrEmpty(phoneNumber))
        {
            string phoneNumberPattern = @"((0093)|(\+93)|(0))[7]\d{8}";

            bool isPhoneNumber = Regex.IsMatch(phoneNumber, phoneNumberPattern);

            return isPhoneNumber;
        }
        else
        {
            return false;
        }
    }
}