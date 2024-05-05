using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class LoginPage : ContentPage
{

    bool isLoggingSucess = false;

    string grantedEmail = "admin";
    string grantedPassword = "1234";

    public LoginPage()
	{
		InitializeComponent();
	}

    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        PasswordEntry.IsPassword = true;
        PasswordEyeIcon.Source = "images/eyeclose.png";

    }

    private async void Login_Button_Clicked(object sender, EventArgs e)
    {

        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await AlertDialogCustom("Login Failed", "Please enter email and password!");
            return;
        }

        if (email == grantedEmail && password == grantedPassword)
        {
            isLoggingSucess = true;
        }
        else
        {
            await AlertDialogCustom("Login Failed", "Email or password is incorrect!");
            return;
        }

        if (isLoggingSucess && App.Current is not null)
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