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


    private async Task PerformLongRunning()
    {
        for (int i = 0; i < 5; i++)
        {
            await Task.Delay(1000);
        }
    }

    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
        //await SetLoading(true);
        await LoadingDialog(true, PerformLongRunning());
        return;
        await PerformRegistration();
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

    
    private async Task PerformRegistration()
    {
        Entry[] formData = [EmailEntry, UsernameEntry, PasswordEntry, ConfirmPasswordEntry];

        // Check if any field is empty
        if (formData.Any(ent => string.IsNullOrEmpty(ent.Text)))
        {
            await AlertDialogCustom("Error", "Please fill in all fields!");
            return;
        }

        foreach(Entry ent in formData)
        {
            ent.Text = ent.Text.Trim();
        }

        // Data validation
        if (!IsUsernameValid(UsernameEntry.Text))
        {
            await AlertDialogCustom("Error", "[Username] can only contain letters, numbers, and underscores (_) with 1-32 characters.");
            return;
        }

        if (!IsEmailValid(EmailEntry.Text))
        {
            await AlertDialogCustom("Error", "Invalid [Email] format! please try again");
            return;
        }


        if (!IsPasswordValid(PasswordEntry.Text))
        {
            await AlertDialogCustom("Error", "[Password] must contain at least one letter, one digit, and be between 6 and 32 characters long.");
            return;
        }

        if (!IsPasswordMatch(PasswordEntry.Text, ConfirmPasswordEntry.Text))
        {
            await AlertDialogCustom("Error", "[Both Passwords] do not match!");
            return;
        }

        await AlertDialogCustom("Registering..", "...");
        // Hash password
        string hashedPassword = HashPassword(PasswordEntry.Text);
        if (hashedPassword == null) {
            await AlertDialogCustom("Error", "Password hashing failed!");
            return;
        }
        await AlertDialogCustom("Success", "Password hashed " + hashedPassword);

    }

    private async Task PerformFirebase()
    {

    }

}