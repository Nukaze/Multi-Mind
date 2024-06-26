using Microsoft.Maui.Controls;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class SplashScreen : ContentPage
{
    public SplashScreen()
    {
        InitializeComponent();
        PerformSplashScreen();
    }

    private async void PerformSplashScreen()
    {
        await Task.Delay(5000);

        //await AlertDialog("Welcome", "Welcome to Multi-Mind", "OK");

        if (Application.Current is null)
        {
            return;
        }

        // override login page but not navigate with appshell
        Application.Current.MainPage = new LoginPage();


        // Start AppShell here if login is successfully 
        //Application.Current.MainPage = new AppShell();
    }

    void ExitMultiMindApp()
    {
        if (Application.Current is null)
        {
            return;
        }
        Application.Current.Quit();
    }
}