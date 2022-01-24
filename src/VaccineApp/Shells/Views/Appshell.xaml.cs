using VaccineApp.Views.Admin.Home.Cluster;
using VaccineApp.Views.Admin.Home.Team;
using VaccineApp.Views.App.Profile;
using VaccineApp.Views.Mobilizer.Home.Area.Clinic;
using VaccineApp.Views.Mobilizer.Home.Area.Doctor;
using VaccineApp.Views.Mobilizer.Home.Area.Influencer;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;
using VaccineApp.Views.Mobilizer.Home.Area.School;
using VaccineApp.Views.Mobilizer.Home.Family;

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

        Tab profile = new()
        {
            Title = "Profile"
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
        ShellContent profilePage = new()
        {
            Title = "Profile",
            Route = nameof(ProfilePage),
            ContentTemplate = new DataTemplate(typeof(ProfilePage))
        };

        profile.Items.Add(profilePage);


        cluster.Items.Add(clusterPage);
        team.Items.Add(teamPage);

        home.Items.Add(cluster);
        home.Items.Add(team);
        home.Items.Add(profile);

        return home;
    }
    public FlyoutItem SupervisorShellStructure()
    {
        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };

        Tab profile = new()
        {
            Title = "Profile"
        };

        ShellContent profilePage = new()
        {
            Title = "Profile",
            Route = nameof(ProfilePage),
            ContentTemplate = new DataTemplate(typeof(ProfilePage))
        };

        profile.Items.Add(profilePage);

        home.Items.Add(profile);
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
        Tab profile = new()
        {
            Title = "Profile"
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

        ShellContent profilePage = new()
        {
            Title = "Profile",
            Route = nameof(ProfilePage),
            ContentTemplate = new DataTemplate(typeof(ProfilePage))
        };

        profile.Items.Add(profilePage);

        home.Items.Add(status);
        home.Items.Add(insights);
        home.Items.Add(family);
        home.Items.Add(area);
        home.Items.Add(profile);

        return home;
    }
    public FlyoutItem ParentShellStructure()
    {
        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };

        Tab profile = new()
        {
            Title = "Profile"
        };

        ShellContent profilePage = new()
        {
            Title = "Profile",
            Route = nameof(ProfilePage),
            ContentTemplate = new DataTemplate(typeof(ProfilePage))
        };

        profile.Items.Add(profilePage);

        home.Items.Add(profile);
        return home;
    }
}