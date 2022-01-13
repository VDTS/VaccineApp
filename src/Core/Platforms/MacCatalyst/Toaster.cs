using Core.Features;

namespace Core;
public class Toaster : IToast
{
    public void MakeToast(string message)
    {
        Application.Current.MainPage.DisplayAlert("", message, "Ok");

    }
}