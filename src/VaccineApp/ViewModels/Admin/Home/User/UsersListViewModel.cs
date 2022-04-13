using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Core.Models;
using FirebaseAdmin.Auth;
using VaccineApp.Views.Admin.User;

namespace VaccineApp.ViewModels.Admin.Home.User;

public partial class UsersListViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<UsersModel> _users;

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
                            Role = user.CustomClaims.ContainsKey("Role") ? user.CustomClaims["Role"].ToString() : "",
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


    [ICommand]
    async void AddUser()
    {
        var route = $"{nameof(AddUserPage)}";
        await Shell.Current.GoToAsync(route);
    }
}
