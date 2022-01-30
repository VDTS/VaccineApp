using VaccineApp.Views.Admin.Home.Cluster;
using VaccineApp.Views.Admin.Home.Team;
using VaccineApp.Views.Admin.Home.User;
using VaccineApp.Views.Admin.User;
using VaccineApp.Views.App;
using VaccineApp.Views.App.AboutUs;
using VaccineApp.Views.App.HelpSupport;
using VaccineApp.Views.App.Profile;
using VaccineApp.Views.App.Settings;
using VaccineApp.Views.Mobilizer.Home.Area.Clinic;
using VaccineApp.Views.Mobilizer.Home.Area.Doctor;
using VaccineApp.Views.Mobilizer.Home.Area.Influencer;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;
using VaccineApp.Views.Mobilizer.Home.Area.School;
using VaccineApp.Views.Mobilizer.Home.Family;
using VaccineApp.Views.Mobilizer.Home.Family.Child;
using VaccineApp.Views.Parent;
using VaccineApp.Views.Supervisor;

namespace VaccineApp.Shells.Views;
public partial class Appshell : Shell
{
    public Appshell(string role)
    {
        InitializeComponent();

        this.Items.Add(CurrentFlyout(role));
    }

    public FlyoutItem CurrentFlyout(string role)
    {
        // Register routes for App pages
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(HelpSupportPage), typeof(HelpSupportPage));
        Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));
        Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(AboutUsPage), typeof(AboutUsPage));

        // Selecting Current Shell based on Role
        if (role == "Admin")
        {
            return AdminShellStructure();
        }
        else if (role == "Supervisor")
        {
            return SupervisorShellStructure();
        }
        else if (role == "Mobilizer")
        {
            return MobilizerShellStructure();
        }
        else if (role == "Parent")
        {
            return ParentShellStructure();
        }
        else
        {
            return new FlyoutItem();
        }
    }

    public FlyoutItem AdminShellStructure()
    {
        // Register routes for App pages
        Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));


        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };
        Tab cluster = new()
        {
            Title = "Cluster"
        };
        Tab team = new()
        {
            Title = "Team"
        };

        Tab user = new()
        {
            Title = "User"
        };

        ShellContent clusterPage = new()
        {
            Title = "Cluster",
            Route = nameof(ClustersListPage),
            ContentTemplate = new DataTemplate(typeof(ClustersListPage))
        };
        ShellContent teamPage = new()
        {
            Title = "Team",
            Route = nameof(TeamsListPage),
            ContentTemplate = new DataTemplate(typeof(TeamsListPage))
        };

        ShellContent userPage = new()
        {
            Title = "User",
            Route = nameof(UsersListPage),
            ContentTemplate = new DataTemplate(typeof(UsersListPage))
        };

        cluster.Items.Add(clusterPage);
        team.Items.Add(teamPage);
        user.Items.Add(userPage);

        home.Items.Add(cluster);
        home.Items.Add(team);
        home.Items.Add(user);

        return home;
    }
    public FlyoutItem SupervisorShellStructure()
    {
        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };

        Tab stats = new()
        {
            Title = "Stats"
        };

        ShellContent statsPage = new()
        {
            Title = "Stats",
            Route = nameof(StatsPage),
            ContentTemplate = new DataTemplate(typeof(StatsPage))
        };

        stats.Items.Add(statsPage);

        home.Items.Add(stats);

        return home;
    }
    public FlyoutItem MobilizerShellStructure()
    {
        // Register those pages on the RouteFactory which are not added to the shell flyout
        Routing.RegisterRoute(nameof(AddFamilyPage), typeof(AddFamilyPage));
        Routing.RegisterRoute(nameof(AddClinicPage), typeof(AddClinicPage));
        Routing.RegisterRoute(nameof(AddDoctorPage), typeof(AddDoctorPage));
        Routing.RegisterRoute(nameof(AddInfluencerPage), typeof(AddInfluencerPage));
        Routing.RegisterRoute(nameof(AddMasjeedPage), typeof(AddMasjeedPage));
        Routing.RegisterRoute(nameof(AddSchoolPage), typeof(AddSchoolPage));
        Routing.RegisterRoute(nameof(FamilyDetailsPage), typeof(FamilyDetailsPage));
        Routing.RegisterRoute(nameof(AddChildPage), typeof(AddChildPage));

        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };
        Tab status = new()
        {
            Title = "Status"
        };
        Tab insights = new()
        {
            Title = "Insights"
        };
        Tab family = new()
        {
            Title = "Family"
        };
        Tab area = new()
        {
            Title = "Area"
        };

        ShellContent familyPage = new()
        {
            Title = "Family",
            Route = nameof(FamiliesListPage),
            ContentTemplate = new DataTemplate(typeof(FamiliesListPage))
        };
        family.Items.Add(familyPage);

        ShellContent clinic = new()
        {
            Title = "Clinic",
            Route = nameof(ClinicsListPage),
            ContentTemplate = new DataTemplate(typeof(ClinicsListPage))
        };
        ShellContent doctor = new()
        {
            Title = "Doctor",
            Route = nameof(DoctorsListPage),
            ContentTemplate = new DataTemplate(typeof(DoctorsListPage))
        };
        ShellContent influencer = new()
        {
            Title = "Influencer",
            Route = nameof(InfluencersListPage),
            ContentTemplate = new DataTemplate(typeof(InfluencersListPage))
        };
        ShellContent masjeed = new()
        {
            Title = "Masjeed",
            Route = nameof(MasjeedsListPage),
            ContentTemplate = new DataTemplate(typeof(MasjeedsListPage))
        };
        ShellContent school = new()
        {
            Title = "School",
            Route = nameof(SchoolsListPage),
            ContentTemplate = new DataTemplate(typeof(SchoolsListPage))
        };

        area.Items.Add(clinic);
        area.Items.Add(doctor);
        area.Items.Add(influencer);
        area.Items.Add(masjeed);
        area.Items.Add(school);

        home.Items.Add(status);
        home.Items.Add(insights);
        home.Items.Add(family);
        home.Items.Add(area);

        return home;
    }
    public FlyoutItem ParentShellStructure()
    {
        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };

        Tab family = new()
        {
            Title = "Family"
        };

        ShellContent familyPage = new()
        {
            Title = "Family",
            Route = nameof(ParentFamilyPage),
            ContentTemplate = new DataTemplate(typeof(ParentFamilyPage))
        };

        family.Items.Add(familyPage);
        home.Items.Add(family);

        return home;
    }
}