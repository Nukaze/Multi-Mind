using Multi_Mind.Models;
using Multi_Mind.Services;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class UserProfile : ContentPage
{
    private User user = App.CurrentUser;
    private DatabaseService _databaseService = new DatabaseService();

    private bool isEditing = false;

    private string tempUsername = "";
    private string tempPassword = "";



    public UserProfile()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await AlertDialogCustom("Welcome", "Welcome to your profile page\n" + user.Username);
        await FetchUserInformationAsync();

    }

    private async Task FetchUserInformationAsync()
    {
        if (user is null)
        {
            await AlertDialogCustom("Error", "User is not loaded");
            return;
        }
        UsernameEntry.Text = user.Username;
        //UsernameEntry.IsEnabled = false;
        UsernameEntry.TextColor = Colors.DarkSlateGray;
        EmailEntry.Text = user.Email;
        PasswordEntry.Text = user.HashedPassword;
    }


    private void accountDeleteBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void accountEditBtn_Clicked(object sender, EventArgs e)
    {

    }
}