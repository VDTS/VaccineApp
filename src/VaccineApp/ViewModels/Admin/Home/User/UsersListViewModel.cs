using Core.Models;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaccineApp.Factory;
using VaccineApp.ViewModels.Base;
using VaccineApp.Views.Admin.Home.User;

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
        Users = new();

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
