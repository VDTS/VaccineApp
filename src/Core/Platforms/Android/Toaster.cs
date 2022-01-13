using Android.Widget;
using Core.Features;

namespace Core;
public class Toaster : IToast
{
    public void MakeToast(string message)
    {
        Toast.MakeText(Platform.AppContext, message, ToastLength.Long).Show();
    }
}