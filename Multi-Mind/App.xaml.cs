using Multi_Mind.Models;
using Multi_Mind.Services;

namespace Multi_Mind
{
    public partial class App : Application
    {

        public static DatabaseService DB { get; private set; }

        public static User CurrentUser { get; set; }


        public App()
        {
            InitializeComponent();
            

            DB = new DatabaseService();

            // init SplashScreen and then init AppShell after
            MainPage = new Views.SplashScreen();
        }
    }
}
