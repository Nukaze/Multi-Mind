using Multi_Mind.Models;
using Multi_Mind.Services;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class UserProfile : ContentPage
{
    private User user = App.CurrentUser;
    private DatabaseService _databaseService = new DatabaseService();

    private readonly Color infoColor = Colors.DarkSlateGray;

    private bool isEditing = false;
    private bool isSubmitEdit = false;
    private bool isPasswordHidden = true;
    private bool isSubmitSuccess = false;

    private string originUsername = "";
    private string originEmail = "";
    private string originPassword = "";




    public UserProfile()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _databaseService.InitializeConnection();

        await FetchUserInformationAsync(user.Email);

    }

    private async Task FetchUserInformationAsync(string email)
    {

        user = await _databaseService.GetUserByEmailAsync(user.Email);

        UsernameEntry.Text = user.Username;
        EmailEntry.Text = user.Email;
        PasswordEntry.Text = user.HashedPassword;
        HiddenPasswordBtn.Source = "eyeclose.png";

        originUsername = user.Username;
        originEmail = user.Email;
        originPassword = user.HashedPassword;
    }


    private async void accountDeleteBtn_Clicked(object sender, EventArgs e)
    {
        bool isConfirmDeleteAccount = await AlertDialogCustom("Notify", "Are you sure you want to DELETE your account?", "Confirm DELETE", "Not DELETE");

        if (isConfirmDeleteAccount)
        {
            await LoadingDialog(true, PerformDeletion, 400);
        }
    }

    private async Task PerformDeletion()
    {
        await _databaseService.InitializeConnection();
        await _databaseService.DeleteRecord(user);
        App.CurrentUser = null;
        Global.CURRENT_USER = null;
        await AlertDialogCustom("Notify", "Account has been deleted successfully");

        App.Current.MainPage = new SplashScreen();
    }



    private async void accountEditBtn_Clicked(object sender, EventArgs e)
    {
        isEditing = !isEditing;
        

        if (isEditing)
        {
            accountEditBtn.Text = "Save the changes";
            accountEditBtn.BackgroundColor = Colors.Teal;

            HiddenPasswordBtn.IsVisible = true;
            HiddenPasswordBtn.IsEnabled = true;

            // unlock the username fields
            UsernameEntry.IsEnabled = true;
            UsernameEntry.IsReadOnly = false;
            UsernameEntry.TextColor = Colors.Black;
            // unlock the password fields
            PasswordEntry.IsEnabled = true;
            PasswordEntry.IsReadOnly = false;
            PasswordEntry.TextColor = Colors.Black;

            // show the cancel cancel button
            accountCancelEdit.IsVisible = true;
            accountCancelEdit.IsEnabled = true;

            await AlertDialogCustom("Notify", "Now you're in edit mode");
        } 
        else
        {
            if (accountEditBtn.Text == "Save the changes")
            {
                bool isAcceptUserChange = await AlertDialogCustom("Notify", 
                    $"Do you want to update your profile?",
                    "Yes",
                    "No"
                    );

                if (isAcceptUserChange)
                {
                    await LoadingDialog(true, PerformSubmitEditProfile, 400);
                }

                ResetUiState();

                return;
            }


        }

    }


    private void accountCancelEdit_Clicked(object sender, EventArgs e)
    {
        isEditing = false;
        isSubmitEdit = false;

        ResetUiState();

        accountCancelEdit.IsVisible = false;
    }

    private async Task PerformSubmitEditProfile()
    {
        isSubmitEdit = true;

        string _username = UsernameEntry.Text;
        string _password = PasswordEntry.Text;

        // validation
        if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
        {
            await AlertDialogCustom("Error", "Username and Password cannot be empty");
            return;
        }

        if (originUsername == _username && originPassword == _password)
        {
            await AlertDialogCustom("Notify", "Nothing changed");
            RefreshPage();
            return;
        }

        if (!IsUsernameValid(_username))
        {
            await AlertDialogCustom("Error", "Username must contain letters, numbers, or underscores (_) and be between 1 and 32 characters in length.");
            return;
        }
        if (!IsPasswordValid(_password))
        {
            await AlertDialogCustom("Error", "Password must contain both letters and numbers and be between 6 and 32 characters in length.");
            return;
        }

        User updatedUser = new User()
        {
            Uid = user.Uid,
            Username = _username,
            Email = user.Email,
            HashedPassword = _password
        };

        // Save the user changes
        await _databaseService.UpdateRecord(updatedUser);
        await AlertDialogCustom("Notify", "Profile has been updated successfully");
    }



    private async void accountLogoutBtn_Clicked(object sender, EventArgs e)
    {
        await AlertDialogCustom("Notify", 
            "Are you sure you want to logout?", 
            "Yes", 
            "No", 
            onAccept: PerformLogout
            );
    }
    private async Task PerformLogout()
    {
        App.CurrentUser = null;
        Global.CURRENT_USER = null;
        App.Current.MainPage = new LoginPage();
    }

    private void HiddenPasswordBtn_Clicked(object sender, EventArgs e)
    {
        isPasswordHidden = !isPasswordHidden;
        PasswordEntry.IsPassword = isPasswordHidden;
        HiddenPasswordBtn.Source = isPasswordHidden ? "eyeclose.png" : "eyeopen.png";
    }

    private async void RefreshPage()
    {
        await Shell.Current.GoToAsync("//Profile");
    }

    private void ResetUiState()
    {
        isSubmitEdit = false;
        isEditing = false;

        isPasswordHidden = true;
        PasswordEntry.IsPassword = isPasswordHidden;
        HiddenPasswordBtn.Source = isPasswordHidden ? "eyeclose.png" : "eyeopen.png";


        accountEditBtn.Text = "Edit Profile";
        accountEditBtn.BackgroundColor = Colors.DarkGoldenrod;
        accountEditBtn.TextColor = Colors.White;

        UsernameEntry.Text = originUsername;
        UsernameEntry.TextColor = infoColor;
        UsernameEntry.IsReadOnly = true;

        PasswordEntry.Text = originPassword;
        PasswordEntry.TextColor = infoColor;
        PasswordEntry.IsReadOnly = true;

        HiddenPasswordBtn.IsVisible = true;
        HiddenPasswordBtn.IsEnabled = false;

        accountCancelEdit.IsVisible = false;
    }
    
}