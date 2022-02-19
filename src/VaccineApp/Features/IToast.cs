namespace VaccineApp.Features;
public interface IToast
{
    void MakeToast(string message);
    void MakeToast(string title, string message);
}
