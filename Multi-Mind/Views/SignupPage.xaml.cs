using static Multi_Mind.Services.Utilize;

using Multi_Mind.Models;

namespace Multi_Mind.Views;

public partial class SignupPage : ContentPage
{
    private User newUser = new User();

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
        bool isRegisterValid = await PerformRegistrationValidation();
        if (isRegisterValid)
        {
            string k = GenerateUniqueId();
            await AlertDialogCustom("Registering..", $"uid {k} \n{newUser.Username}\n {newUser.Email} \n {newUser.HashedPassword}");

            await AlertDialogCustom("db", App.DB._conn.ToString());
            await PerformSQLiteRegistration();
        }
        
        return;
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


    private async Task<bool> PerformRegistrationValidation()
    {
        Entry[] formData = [EmailEntry, UsernameEntry, PasswordEntry, ConfirmPasswordEntry];

        // Check if any field is empty
        if (formData.Any(ent => string.IsNullOrEmpty(ent.Text)))
        {
            await AlertDialogCustom("Error", "Please fill in all fields!");
            return false;
        }

        foreach(Entry ent in formData)
        {
            ent.Text = ent.Text.Trim();
        }

        // Data validation
        if (!IsUsernameValid(UsernameEntry.Text))
        {
            await AlertDialogCustom("Error", "[Username] can only contain letters, numbers, and underscores (_) with 1-32 characters.");
            return false;
        }

        if (!IsEmailValid(EmailEntry.Text))
        {
            await AlertDialogCustom("Error", "Invalid [Email] format! please try again");
            return false;
        }


        if (!IsPasswordValid(PasswordEntry.Text))
        {
            await AlertDialogCustom("Error", "[Password] must contain at least one letter, one digit, and be between 6 and 32 characters long.");
            return false;
        }

        if (!IsPasswordMatch(PasswordEntry.Text, ConfirmPasswordEntry.Text))
        {
            await AlertDialogCustom("Error", "[Both Passwords] do not match!");
            return false;
        }

        // Hash password
        string hashedPassword = HashPassword(PasswordEntry.Text);
        if (hashedPassword == null)
        {
            await AlertDialogCustom("Error", "Password hashing failed!");
            return false;
        }

        
        newUser.SetAll(UsernameEntry.Text, EmailEntry.Text, hashedPassword);

        if (newUser.IsAnyPropertyEmpty())
        {
            await AlertDialogCustom("Error", "User creation failed!");
            App.Current.MainPage = new SignupPage();
            return false;
        }

        return true;
    }

    private async Task PerformSQLiteRegistration()
    {
        User s = await App.DB.GetUsersAsync();
        AlertDialogCustom("User", $"{s.Username} {s.Email} {s.HashedPassword}");

    }

}