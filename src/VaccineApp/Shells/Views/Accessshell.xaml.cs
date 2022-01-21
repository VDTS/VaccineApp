using VaccineApp.Views.Access.SignIn;

namespace VaccineApp.Shells.Views;

public partial class Accessshell : Shell
{
	public Accessshell()
	{
		InitializeComponent();

        this.Items.Add(AccessShell());
	}

    public FlyoutItem AccessShell()
    {
        FlyoutItem home = new();
        Tab signin = new();
        ShellContent signinPage = new()
        {
            Title = "Sign In",
            Route = nameof(SignInPage),
            ContentTemplate = new DataTemplate(typeof(SignInPage))
        };
        signin.Items.Add(signinPage);
        home.Items.Add(signin);

        return home;
    }
}
