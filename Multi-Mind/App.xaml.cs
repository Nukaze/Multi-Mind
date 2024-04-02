namespace Multi_Mind
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // init SplashScreen and then init AppShell after
            MainPage = new Views.SplashScreen();
        }
    }
}
