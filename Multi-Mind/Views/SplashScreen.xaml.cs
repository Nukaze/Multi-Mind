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

        await AlertDialog("Welcome", "Welcome to Multi-Mind", "OK");
        await AlertDialog("Are you want to", "Exit", "OK", "Cancel", ExitMultiMindApp);
        if (Application.Current is null)
        {
            return;
        }
        Application.Current.MainPage = new AppShell();
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