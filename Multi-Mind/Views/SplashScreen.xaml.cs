using Microsoft.Maui.Controls;
using System.Threading.Tasks;


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
        if (Application.Current is null)
        {
            return;
        }
        Application.Current.MainPage = new AppShell();
    }
}