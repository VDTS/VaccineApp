using VaccineApp.Views.Admin.Home.Cluster;
using VaccineApp.Views.Admin.Home.Team;
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
            Title = "Home"
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

        cluster.Items.Add(clusterPage);
        team.Items.Add(teamPage);

        home.Items.Add(cluster);
        home.Items.Add(team);

        return home;
    }
    public FlyoutItem SupervisorShellStructure()
    {
        return new FlyoutItem();
    }
    public FlyoutItem MobilizerShellStructure()
    {
        FlyoutItem home = new()
        {
            Title = "Home"
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

        home.Items.Add(status);
        home.Items.Add(insights);
        home.Items.Add(family);
        home.Items.Add(area);
        home.Items.Add(profile);

        return home;
    }
    public FlyoutItem ParentShellStructure()
    {
        return new FlyoutItem();
    }
}