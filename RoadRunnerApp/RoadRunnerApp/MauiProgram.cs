using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace RoadRunnerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
                        .ConfigureImageSources((context, sources) =>
                         {
                             // Voeg het pad naar je app-pictogram toe
                             sources.Add("appicon", "Resources/your_app_icon.png");
                         });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
