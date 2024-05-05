using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        PasswordEntry.IsPassword = true;
        ConfirmPasswordEntry.IsPassword = true;

        PasswordIcon.Source = "images/eyeclose.png";
        ConfirmPasswordIcon.Source = "images/eyeclose.png";
    }

    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
        await AlertDialogCustom("Notify", "Signing up!");
    }

    private async void LoginLabel_Clicked(object sender, TappedEventArgs e)
    {
        if (App.Current is null)
        {
            await AlertDialogCustom("Error", "App.Current is null");
            return; 
        }

        App.Current.MainPage = new LoginPage();
        return;
    }

    private void PasswordIcon_Clicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        PasswordIcon.Source = PasswordEntry.IsPassword ? "images/eyeclose.png" : "images/eyeopen.png";
    }

    private void ConfirmPasswordIcon_Clicked(object sender, EventArgs e)
    {
        ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
        ConfirmPasswordIcon.Source = ConfirmPasswordEntry.IsPassword ? "images/eyeclose.png" : "images/eyeopen.png";
    }
}