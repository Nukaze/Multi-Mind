using Microsoft.Extensions.Logging;

namespace Multi_Mind
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("LINESeedSansTH.ttf", "LINESeedSansTH");
                    fonts.AddFont("LINESeedSansTH_Rg.ttf", "LINESeedSansTH_Rg");
                    fonts.AddFont("LINESeedSansTH_Th.ttf", "LINESeedSansTH_Th");
                    fonts.AddFont("Batangas_700.otf", "Batangas");
                    fonts.AddFont("Blanka-Regular.otf", "Blanka");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
