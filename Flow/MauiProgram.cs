using Microsoft.Extensions.Logging;

namespace Flow
{
    using CommunityToolkit.Maui;
    using Flow.Local_Database;
    using Flow.ViewModels;
    using Flow.Views;

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            builder

            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code

                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Continue initializing your .NET MAUI App here


            return builder.Build();
        }
    }
}
