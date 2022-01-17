using Core.Features;

namespace Core;
public class Toaster : IToast
{
    public void MakeToast(string message)
    {
        Application.Current.MainPage.DisplayAlert("", message, "Ok");
    }
    public void MakeToast(string title, string message)
    {
        Application.Current.MainPage.DisplayAlert(title, message, "Ok");
    }
}