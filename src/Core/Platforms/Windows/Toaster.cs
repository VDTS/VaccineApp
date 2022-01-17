using Core.Features;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Core;
public class Toaster : IToast
{
    public void MakeToast(string message)
    {
        var xmdock = CreateToast(message);
        var toast = new ToastNotification(xmdock);

        var notifi = ToastNotificationManager.CreateToastNotifier();
        notifi.Show(toast);
    }

    public void MakeToast(string title, string message)
    {
        var xmdock = CreateToast(title, message);
        var toast = new ToastNotification(xmdock);

        var notifi = ToastNotificationManager.CreateToastNotifier();
        notifi.Show(toast);
    }

    public static XmlDocument CreateToast(string message)
    {
        var xDoc = new XDocument(
           new XElement("toast",
           new XElement("visual",
           new XElement("binding", new XAttribute("template", "ToastGeneric"),
           new XElement("text", message))),
           
           new XElement("actions",
                new XElement("action", new XAttribute("activationType", "background"),
                new XAttribute("content", "Ok"), new XAttribute("arguments", "Ok"))
                )));

        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xDoc.ToString());
        return xmlDoc;
    }
    public static XmlDocument CreateToast(string title, string message)
    {
        var xDoc = new XDocument(
           new XElement("toast",
           new XElement("visual",
           new XElement("binding", new XAttribute("template", "ToastGeneric"),
           new XElement("text", title),
           new XElement("text", message))),

           new XElement("actions",
                new XElement("action", new XAttribute("activationType", "background"),
                new XAttribute("content", "Ok"), new XAttribute("arguments", "Ok"))
                )));

        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xDoc.ToString());
        return xmlDoc;
    }
}
