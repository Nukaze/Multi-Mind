using static Multi_Mind.Services.Utilize;

using Multi_Mind.Models;
using Multi_Mind.Services;

namespace Multi_Mind.Views;

public partial class SignupPage : ContentPage
{
    private User newUser = new User();
    private readonly DatabaseService _databaseService = new DatabaseService();

    private bool isRegistering = false;

    public SignupPage()
	{
        InitializeComponent();
        initDatabaseService();
	}

    private async void initDatabaseService()
    {
        await _databaseService.InitializeConnection();
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
        // Prevent multiple registration by checking the flag state
        if (isRegistering)
        {
            return;
        }

        // Setting a flag state
        isRegistering = true;
        // Perform registration
        await LoadingDialog(true, PerformSQLiteRegistration, 1000);
        // Reset the flag
        isRegistering = false;

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
        // for testing
        string plainedPassword = PasswordEntry.Text;

        
        newUser.SetAll(UsernameEntry.Text, EmailEntry.Text, plainedPassword);

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
        bool isRegisterFormValid = await PerformRegistrationValidation();
        if (!isRegisterFormValid)
        {
            return;
        }
        try
        {
            bool isEmailExisted = await _databaseService.IsEmailExisted(newUser.Email);
            if (isEmailExisted)
            {
                await AlertDialogCustom("Error", "Email is already registered!");
                return;
            }

            // Register the new user
            int result = await _databaseService.Create(newUser);
            if (result > 0)
            {
                await AlertDialogCustom("Successfully", "User registered successfully!");
                // Navigate to a different page after successful registration
                User storedNewUser = await _databaseService.GetUserByEmailAsync(newUser.Email);

                if (storedNewUser is null) {
                    await AlertDialogCustom("Error", "User registration failed! Please try again later.");
                    return;
                }

                await AlertDialogCustom("Successfully", $"{storedNewUser.Username} has been created\ngoing to login..");
                App.Current.MainPage = new LoginPage();
                return;

            }
            else
            {
                await AlertDialogCustom("Error", "User registration failed! Please try again later.");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during registration
            await AlertDialogCustom("Error", $"An error occurred: {ex.Message}");
        }

    }

}