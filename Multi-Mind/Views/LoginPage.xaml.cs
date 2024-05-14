using Multi_Mind.Models;
using Multi_Mind.Services;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class LoginPage : ContentPage
{

    bool isLoggingSuccess = false;

    string grantedTempEmail = "admin";
    string grantedTempPassword = "1234";

    private readonly DatabaseService _databaseService = new DatabaseService();


    public LoginPage()
	{
		InitializeComponent();
        _databaseService.InitializeConnection();
	}

    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        PasswordEntry.IsPassword = true;
        PasswordEyeIcon.Source = "images/eyeclose.png";

    }

    private async void Login_Button_Clicked(object sender, EventArgs e)
    {
        await LoadingDialog(true, LoginProcess);

    }

    private async Task LoginProcess()
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await AlertDialogCustom("Login Failed", "Please enter email and password!");
            return;
        }

        if (email == grantedTempEmail && password == grantedTempPassword)
        {
            isLoggingSuccess = true;
        }

        User user = await _databaseService.GetUserByEmailAsync(email);
        if (user is null)
        {
            await AlertDialogCustom("Login Failed", "User not found!");
            return;
        }
        if (user.HashedPassword != password)
        {
            await AlertDialogCustom("Login Failed", "Password is invalid!");
            return;
        }

        App.CurrentUser = user;
        isLoggingSuccess = true;

        await AlertDialogCustom("Login Success", "Going to AI Hub");
        
        if (isLoggingSuccess && App.CurrentUser is not null)
        {
            App.Current.MainPage = new AppShell();
        }
    }



    private async void Register_Button_Clicked(object sender, TappedEventArgs e)
    {
        if (App.Current is null)
        {
            await AlertDialogCustom("Error", "App.Current is null");
            return;
        }

        App.Current.MainPage = new SignupPage();
        return;
    }

    private void PasswordEyeIcon_Clicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        PasswordEyeIcon.Source = (PasswordEntry.IsPassword) ? "images/eyeclose.png" : "images/eyeopen.png";
    }
}