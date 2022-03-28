using VaccineApp.Views.Admin.Home.Cluster;
using VaccineApp.Views.Admin.Home.Team;
using VaccineApp.Views.Admin.User;
using VaccineApp.Views.App;
using VaccineApp.Views.App.AboutUs;
using VaccineApp.Views.App.HelpSupport;
using VaccineApp.Views.App.Profile;
using VaccineApp.Views.App.Settings;
using VaccineApp.Views.Mobilizer.Announcements;
using VaccineApp.Views.Mobilizer.Home.Area.Clinic;
using VaccineApp.Views.Mobilizer.Home.Area.Doctor;
using VaccineApp.Views.Mobilizer.Home.Area.Influencer;
using VaccineApp.Views.Mobilizer.Home.Area.Masjeed;
using VaccineApp.Views.Mobilizer.Home.Area.School;
using VaccineApp.Views.Mobilizer.Home.Family;
using VaccineApp.Views.Mobilizer.Home.Family.Child;
using VaccineApp.Views.Mobilizer.Home.Status;
using VaccineApp.Views.Mobilizer.Home.Status.AnonymousChild;
using VaccineApp.Views.Mobilizer.Home.Status.Vaccine;
using VaccineApp.Views.Parent;
using VaccineApp.Views.Parent.Guides;
using VaccineApp.Views.Parent.QR;
using VaccineApp.Views.Parent.VaccineNotifications;
using VaccineApp.Views.Supervisor;
using VaccineApp.Views.Supervisor.Announcements;
using VaccineApp.Views.Supervisor.Charts;
using VaccineApp.Views.Supervisor.Periods;
using VaccineApp.Views.Supervisor.Reports;

namespace VaccineApp.Shells.Views;
public partial class Appshell : Shell
{
    public Appshell(string role)
    {
        InitializeComponent();

        foreach (var item in CurrentFlyout(role))
        {
            this.Items.Add(item);
        }
    }

    public IEnumerable<FlyoutItem> CurrentFlyout(string role)
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
            return new List<FlyoutItem>();
        }
    }

    public IEnumerable<FlyoutItem> AdminShellStructure()
    {
        // Register routes for App pages
        Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));
        Routing.RegisterRoute(nameof(AddClusterPage), typeof(AddClusterPage));
        Routing.RegisterRoute(nameof(AddTeamPage), typeof(AddTeamPage));


        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };
        Tab cluster = new()
        {
            Title = "Cluster",
            Icon = "cluster.png"
        };
        Tab team = new()
        {
            Title = "Team",
            Icon = "team.png"
        };

        Tab user = new()
        {
            Title = "User",
            Icon = "users.png"
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

        var list = new List<FlyoutItem>();
        list.Add(home);

        return list;
    }
    public IEnumerable<FlyoutItem> SupervisorShellStructure()
    {
        // Registering nested pages
        Routing.RegisterRoute(nameof(AddAnnouncementPage), typeof(AddAnnouncementPage));
        Routing.RegisterRoute(nameof(AddPeriodPage), typeof(AddPeriodPage));


        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };

        Tab stats = new()
        {
            Title = "Stats",
            Icon = "stats.png"
        };

        Tab charts = new()
        {
            Title = "Charts",
            Icon = "chart.png"
        };

        Tab reports = new()
        {
            Title = "Reports",
            Icon = "report.png"
        };

        Tab period = new()
        {
            Title = "Periods",
            Icon = "period.png"
        };

        Tab announcement = new()
        {
            Title = "Announcements",
            Icon = "announcement.png"
        };

        ShellContent statsPage = new()
        {
            Title = "Stats",
            Route = nameof(StatsPage),
            ContentTemplate = new DataTemplate(typeof(StatsPage))
        };

        ShellContent chartsPage = new()
        {
            Title = "Charts",
            Route = nameof(ChartsPage),
            ContentTemplate = new DataTemplate(typeof(ChartsPage))
        };

        ShellContent reportsPage = new()
        {
            Title = "Reports",
            Route = nameof(ReportsPage),
            ContentTemplate = new DataTemplate(typeof(ReportsPage))
        };

        ShellContent announcementPage = new()
        {
            Title = "Announcement",
            Route = nameof(AnnouncementsListPage),
            ContentTemplate = new DataTemplate(typeof(AnnouncementsListPage))
        };

        ShellContent periodPage = new()
        {
            Title = "Period",
            Route = nameof(PeriodsListPage),
            ContentTemplate = new DataTemplate(typeof(PeriodsListPage))
        };

        stats.Items.Add(statsPage);
        charts.Items.Add(chartsPage);
        reports.Items.Add(reportsPage);
        period.Items.Add(periodPage);
        announcement.Items.Add(announcementPage);

        home.Items.Add(stats);
        home.Items.Add(charts);
        home.Items.Add(reports);
        home.Items.Add(period);
        home.Items.Add(announcement);

        var list = new List<FlyoutItem>();
        list.Add(home);

        return list;
    }
    public IEnumerable<FlyoutItem> MobilizerShellStructure()
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
        Routing.RegisterRoute(nameof(AddAnonymousChildPage), typeof(AddAnonymousChildPage));
        Routing.RegisterRoute(nameof(ChildDetailsPage), typeof(ChildDetailsPage));
        Routing.RegisterRoute(nameof(AddVaccinePage), typeof(AddVaccinePage));
        Routing.RegisterRoute(nameof(MasjeedDetailsPage), typeof(MasjeedDetailsPage));
        Routing.RegisterRoute(nameof(SchoolDetailsPage), typeof(SchoolDetailsPage));
        Routing.RegisterRoute(nameof(ClinicDetailsPage), typeof(ClinicDetailsPage));
        Routing.RegisterRoute(nameof(VaccineDetailsPage), typeof(VaccineDetailsPage));

        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };

        FlyoutItem announcementFlyout = new()
        {
            Title = "Announcements",
            Icon = "announcement.png"
        };

        Tab status = new()
        {
            Title = "Status",
            Icon = "status.png"
        };

        Tab family = new()
        {
            Title = "Family",
            Icon = "family.png"
        };

        Tab area = new()
        {
            Title = "Area",
            Icon = "area.png"
        };

        Tab announcement = new()
        {
            Title = "Announcements"
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

        ShellContent announcementPage = new()
        {
            Title = "Announcements",
            Route = nameof(AnnouncementListPage),
            ContentTemplate = new DataTemplate(typeof(AnnouncementListPage))
        };

        ShellContent statusPage = new()
        {
            Title = "Status",
            Route = nameof(StatusPage),
            ContentTemplate = new DataTemplate(typeof(StatusPage))
        };

        ShellContent anonymousPage = new()
        {
            Title = "Anonymous Child",
            Route = nameof(AnonymousChildrenListPage),
            ContentTemplate = new DataTemplate(typeof(AnonymousChildrenListPage))
        };

        area.Items.Add(clinic);
        area.Items.Add(doctor);
        area.Items.Add(influencer);
        area.Items.Add(masjeed);
        area.Items.Add(school);
        announcement.Items.Add(announcementPage);

        status.Items.Add(statusPage);
        status.Items.Add(anonymousPage);

        home.Items.Add(status);
        home.Items.Add(family);
        home.Items.Add(area);
        announcementFlyout.Items.Add(announcement);

        var list = new List<FlyoutItem>();
        list.Add(home);
        list.Add(announcementFlyout);

        return list;
    }
    public IEnumerable<FlyoutItem> ParentShellStructure()
    {
        // Register routes for App pages
        Routing.RegisterRoute(nameof(QRGeneratedImagePage), typeof(QRGeneratedImagePage));
        Routing.RegisterRoute(nameof(ParentChildDetailsPage), typeof(ParentChildDetailsPage));
      
        FlyoutItem home = new()
        {
            Title = "Home",
            Icon = "home.png"
        };

        Tab family = new()
        {
            Title = "Family",
            Icon = "family.png"
        };

        Tab guides = new()
        {
            Title = "Guides",
            Icon = "guides.png"
        };

        Tab notifications = new()
        {
            Title = "Vaccine Notifications",
            Icon = "notification.png"
        };

        ShellContent familyPage = new()
        {
            Title = "Family",
            Route = nameof(ParentFamilyPage),
            ContentTemplate = new DataTemplate(typeof(ParentFamilyPage))
        };

        ShellContent vaccinesTimeTablePage = new()
        {
            Title = "Vaccines TimeTable",
            Route = nameof(VaccinesTimeTablePage),
            ContentTemplate = new DataTemplate(typeof(VaccinesTimeTablePage))
        };

        ShellContent vaccineGuidesPage = new()
        {
            Title = "Guides",
            Route = nameof(VaccineGuidesPage),
            ContentTemplate = new DataTemplate(typeof(VaccineGuidesPage))
        };

        ShellContent vaccinesNotificationsPage = new()
        {
            Title = "Vaccines Notification",
            Route = nameof(VaccineNotificationsPage),
            ContentTemplate = new DataTemplate(typeof(VaccineNotificationsPage))
        };

        family.Items.Add(familyPage);
        guides.Items.Add(vaccinesTimeTablePage);
        guides.Items.Add(vaccineGuidesPage);
        notifications.Items.Add(vaccinesNotificationsPage);

        home.Items.Add(family);
        home.Items.Add(guides);
        home.Items.Add(notifications);

        var list = new List<FlyoutItem>();
        list.Add(home);

        return list;
    }
}