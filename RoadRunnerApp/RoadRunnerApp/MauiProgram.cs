using Google.Api.Gax.Grpc.Rest;
using Google.Maps.Routing.V2;
using Microsoft.Extensions.Logging;
using RoadRunnerApp.Views;
using RoadRunnerApp.Views.Models;

namespace RoadRunnerApp
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
                }).UseMauiMaps();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
    
            return builder.Build();
        }
    }
}
