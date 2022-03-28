using System.Collections.ObjectModel;
using System.Windows.Input;
using Core.Models;
using FirebaseAdmin.Auth;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Admin.User;

namespace VaccineApp.ViewModels.Admin.Home.User;

public class UsersListViewModel : ViewModelBase
{
    private ObservableCollection<UsersModel> _users;

    public UsersListViewModel()
    {
        AddUserCommand = new Command(AddUser);
    }

    public async void Get()
    {
        try
        {
            var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
            var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();
            while (await responses.MoveNextAsync())
            {
                ExportedUserRecords response = responses.Current;
                foreach (ExportedUserRecord user in response.Users)
                {
                    Users.Add(
                        new UsersModel
                        {
                            UId = user.Uid,
                            DisplayName = user.DisplayName,
                            Email = user.Email,
                            Role = user.CustomClaims["Role"]?.ToString(),
                            EmailVerified = user.EmailVerified
                        }
                        );
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Clear()
    {
        Users = new();
    }

    private async void AddUser()
    {
        var route = $"{nameof(AddUserPage)}";
        await Shell.Current.GoToAsync(route);
    }

    public ICommand AddUserCommand { private set; get; }
    public ObservableCollection<UsersModel> Users
    {
        get { return _users; }
        set { _users = value; OnPropertyChanged(); }
    }
}
