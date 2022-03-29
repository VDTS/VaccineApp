namespace VaccineApp.ViewModels.App.AboutUs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class AboutUsViewModel : ObservableObject
{

    [ObservableProperty]
    List<Person> _persons;

    public AboutUsViewModel()
    {
        Get();
    }

    void Get()
    {
        Persons = new List<Person>()
        {
            new Person{
                FullName = "Naveed Ahmad Hematmal",
                PhotoURL = "naveedahmad.jpg",
                Role = "Project Manager & Full Stack Developer",
                LinkedInURL = "https://linkedin.com/in/naveedahmadhematmal",
                TwitterURL = "https://twitter.com/NaveedHematmal"
            },

            new Person{
                FullName = "Saeeda Rasuly",
                Role = "Program manager and UX designer",
                PhotoURL = "profiledefaultimage.png",
                LinkedInURL = "https://linkedin.com/in/saeeda-rasuly-377327169",
                TwitterURL = "https://twitter.com/RasulySaeeda"
            },

            new Person{
                FullName = "Mohammed Yasin Zahin",
                Role = "Contents Developer and UX Designer",
                PhotoURL = "mohammadyasin.jpg",
                LinkedInURL = "https://linkedin.com/in/mohammad-yasin-zahin-95753517b",
                TwitterURL = "https://twitter.com/YasinZahin4"
            },

            new Person{
                FullName = "Abdul Basir Zafar",
                Role = "Developer and UI Designer",
                PhotoURL = "abdulbasir.jpg",
                LinkedInURL = "https://linkedin.com/in/abdul-basir-zafar-271097193",
                TwitterURL = "https://twitter.com/abBasirZafar"
            }
        };
    }

    [ICommand]
    private async void GoToTwitter(string url)
    {
        Uri uri = new Uri(url);
        await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }

    [ICommand]
    private async void GoToLinkedIn(string url)
    {
        Uri uri = new Uri(url);
        await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
}

public class Person
{
    public string FullName { get; set; }
    public string Role { get; set; }
    public string PhotoURL { get; set; }
    public string LinkedInURL { get; set; }
    public string TwitterURL { get; set; }
}
