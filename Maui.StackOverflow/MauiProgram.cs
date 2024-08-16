using Microsoft.Extensions.Logging;

namespace Maui.StackOverflow
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
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<Maui.StackOverflow.MainPage>();
            builder.Services.AddTransient<Maui.StackOverflow.Answers.RgbColor.RgbColorPage>();

            return builder.Build();
        }
    }
}
